# CJWS - Certificate-based Json Web Signature
This is an **EXPERIMENTAL** and **UNOFFICIAL** extension/modification of the standardized Json Web Signatures ([JWS - RFC7515](https://www.rfc-editor.org/rfc/rfc7515)).

A CJWS can only be created/signed using a valid PKI certificate (not self-signed) that can be verified by its X.509 chain.
As consequence a CJWS can be used for proofs/attestations.

This format can be used for (half-)automated processes, where the receiver does not know the sender. The PKI enables the receiver to verify the identity of the sender and the validity of the data.
Also the CJWS2 format allows the data to be signed by multiple instances, each with its own certificate, and still compact serialization.

In this project two types of Certificate-based Json Web Signatures are defined:
- [CJWS1](#cjws-v1): compatible with the [JWS - RFC7515](https://www.rfc-editor.org/rfc/rfc7515)
- [CJWS2](#cjws-v2): extended/modified JWS strcuture, not compatible

### Example Scenario

The german T-prescription was implemented as an application scenario, since it involves special requirements for signatures, data protection and data transfer. For this scenario, all the necessary applications and libraries are implemented in simple form.

# CJWS v1


## The document
A CJWS1 document consists of the following parts:
- Header
- Payload
- Signature

The element of the document is serialized to URL-safe base64 string. <br />
The strings are combined/joined using the '.' character:
~~~
{HEADER}.{PAYLOAD}.{SIGNATURE}
~~~

## Header
~~~
{
    "typ": "cjws1",
    "cty": "example-document-type",
    "alg": "RS512",
    "x5c": "...[BASE64]...",
    "day": "2023/03/31"
}
~~~
The header is compatible to a JWS header.

- **typ**: The type of the document -> should always be cjws1.
- **cty**: The payload content type; used by the applications.
- **alg**: The algorithm algorithm used to create the signature (this property is informative). <br />
Valid values are: RS256, RS384, RS512, ES256, ES384, ES512.
- **x5c**: The certificate file (CRT, no private key) as base64 encoded byte array.
- **day**: The signature date as string in yyyy/MM/dd format.

## Payload
The payload can either be an object serialized to a json string given as UTF-8 byte array or a binary.  <br />
The content type in the header should be set accordingly.

## Signature
The digital signature (of header + '.' + payload) as base64 encoded byte array.

The contained certificate should be validated against its X.509 chain. <br />
It also allows validating the digital signature for the given payload.

A document is valid if the certificate and digital signature are valid.


# CJWS v2

## The document
A Json Web Document consists of the following parts:
- Header
- Payload
- Signature(s)

The element of the document is serialized to URL-safe base64 string. <br />
The strings are combined/joined using the '.' character:
~~~
{HEADER}.{PAYLOAD}.{SIGNATURE1}.{SIGNATURE2} ...
~~~

This format allows the extraction of the header without reading the complete stream or file and also multiple signatures at different points in time.

## Header
~~~
{
    "typ": "cjws2",
    "cty": "example-document-type"
}
~~~
The header is compatible to a JWS header.

- **typ**: The type of the document -> should always be cjws2.
- **cty**: The payload content type; used by the applications.

## Payload
The payload can either be an object serialized to a json string given as UTF-8 byte array or a binary.  <br />
The content type in the header should be set accordingly.

## Signature
~~~
{
    "alg": "RS512",
    "x5c": "...[BASE64]...",
    "sig": "...[BASE64]...",
    "day": "2023/03/31"
}
~~~
The document can contain multiple signature object. Each signature object has the following properties:

- **alg**: The algorithm algorithm used to create the signature (this property is informative). <br />
Valid values are: RS256, RS384, RS512, ES256, ES384, ES512.
- **x5c**: The certificate file (CRT, no private key) as base64 encoded byte array.
- **sig**: The digital signature (of header, payload and day parameter) as base64 encoded byte array.
- **day**: The signature date as string in yyyy/MM/dd format.

The contained certificate should be validated against its X.509 chain. <br />
It also allows validating the digital signature for the given payload.

A document is valid if ALL certificates and digital signatures are valid.
