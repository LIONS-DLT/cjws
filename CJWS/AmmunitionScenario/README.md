# Ammunition Issuance Scenario

### Description

In many countries, e.g., Germany, ammunition is counted exactly during target practice, both when the ammunition is issued and the rounds fired. Ammunition is only issued to the participants if a valid order has been presented. In this scenario, it is assumed that in the digitized process, the (digital) command is converted into individual digital ammunition requests per participant by the supervisor and signed by him. The transmitted data object contains information about the type and amount of ammunition, as well as the identifier of the participant. The command itself can be contained in the transferred data object as a reference or embedded data object. The participant then obtains his ammunition from the ammunition issuing point and submits his request. This is digitally signed by the ammunition issuing point and forwarded to a registration entity. According to
this process we derive the following criteria for an exchange format: support for multiple signatures; signatures by different identities; signatures at different points in time; availability of certificates; document name and type.

The ammunition requests and receipts are handed over to the participant electronically as a CJWS record. In the superordinate research project in which this research is located, an app for smartphones was developed that serves as a carrier for certificates through which signatures and authentications can be performed. This app has been extended so that also signed CJWS records can be picked up and handed back.

### Applications / Libraries

This scenario implements the following applications and libraries:

- Data Exchange Service (ASP.NET)
- Data Exchange Client (.NET class library)
- Ammunition Scenario Library (.NET class library)
- Instructors's Application (.NET MAUI Application)
- Issuer's Application (.NET MAUI Application)

### Requirements

To test this process a valid certificate is required. The data exchange service must be running and the corresponding URLs must be changed within the source codes.
