using Net.Codecrete.QrCodeGenerator;
using SkiaSharp;

namespace IssuersApplication;

public partial class QrModal : ContentPage
{
    public QrModal(string qrContent)
    {
        InitializeComponent();

        img.Source = ConvertSvgToImageSource(QrCode.EncodeText(qrContent, QrCode.Ecc.Low));
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        App.Current.MainPage.Navigation.PopModalAsync();
    }

    public ImageSource ConvertSvgToImageSource(QrCode qrCode /*byte[] svgData*/)
    {
        var imageStream = new MemoryStream(QrToPng(qrCode, 32, 1, SKColors.Black, SKColors.White));
        var imageSource = ImageSource.FromStream(() => imageStream);
        return imageSource;
    }

    public static byte[] QrToPng(QrCode qrCode, int scale, int border, SKColor foreground, SKColor background)
    {
        using SKBitmap bitmap = QrToBitmap(qrCode, scale, border, foreground, background);
        using SKData data = bitmap.Encode(SKEncodedImageFormat.Png, 90);
        return data.ToArray();
    }

    public static SKBitmap QrToBitmap(QrCode qrCode, int scale, int border, SKColor foreground, SKColor background)
    {
        // check arguments
        if (scale <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(scale), "Value out of range");
        }
        if (border < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(border), "Value out of range");
        }

        int size = qrCode.Size;
        int dim = (size + border * 2) * scale;

        if (dim > short.MaxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(scale), "Scale or border too large");
        }

        // create bitmap
        SKBitmap bitmap = new SKBitmap(dim, dim, SKColorType.Rgb888x, SKAlphaType.Opaque);

        using (SKCanvas canvas = new SKCanvas(bitmap))
        {
            // draw background
            using (SKPaint paint = new SKPaint { Color = background })
            {
                canvas.DrawRect(0, 0, dim, dim, paint);
            }

            // draw modules
            using (SKPaint paint = new SKPaint { Color = foreground })
            {
                for (int y = 0; y < size; y++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        if (qrCode.GetModule(x, y))
                        {
                            canvas.DrawRect((x + border) * scale, (y + border) * scale, scale, scale, paint);
                        }
                    }
                }
            }
        }

        return bitmap;
    }
}