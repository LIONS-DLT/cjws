# CJWS Library (.NET)

## Create and serialize new document
~~~
  CJWS2 cjws = new CJWS2(new CJWS2Header()
  {
      ContentType = "your-document-type",
      DisplayText = "Display Text"
  });
  cjws.SetPayloadObject(payloadObject);
  cjws.Sign(cert1, HashAlgorithmName.SHA512);
  cjws.Sign(cert2, HashAlgorithmName.SHA512);

  string cjwsString = cjws.Serialize();
~~~

## Deserialize and verify document
~~~
  CJWSHeaderInfo info = CJWS.ExtractHeaderFromString(cjwsString);
  
  if (info.Type != "cjws2")
      throw new Exception("Invalid format.");
      
  if (info.ContentType != "your-document-type")
      throw new Exception("Invalid content type.");
      
  CJWS2 document = CJWS2.Deserialize(cjwsString);
  if(!document.Verify())
      throw new Exception("Invalid signature or certificate.");
  
  YourPayloadObject payload = document.GetPayloadObject<YourPayloadObject>();
~~~
