# T-Prescription Scenario

### Description

Physicians may only prescribe drugs containing lenalidomide and thalidomide on a numbered two-part official form issued by the german BfArM. As with a normal prescription, the original is given to the respective health insurance company. Additionally a carbon copy, on which the patient data are blacked out, is forwarded to the BfArM by the pharmacy on a quarterly basis.

The prescription and the carbon copy are handed over to the patient electronically as a CJWS record. In the superordinate research project in which this research is located, an app for smartphones was developed that serves as a carrier for certificates through which signatures and authentications can be performed. This app has been extended so that also signed CJWS records can be picked up and handed back.

### Applications / Libraries

This scenario implements the following applications and libraries:

- Data Exchange Service (ASP.NET)
- Data Exchange Client (.NET class library)
- Prescription Library (.NET class library)
- Doctor's Application (.NET WinForms Application)
- Pharmacist's Application (.NET WinForms Application)

### Requirements

To test this process a valid PKI certificate is required.
