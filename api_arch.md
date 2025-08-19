/HISApiGateway
|-- /src
|-- /APIGateway
|-- APIGateway.csproj
|-- Startup.cs
|-- appsettings.json
|-- /Common
|-- Common.csproj
|-- // Shared utilities and models
|-- Dockerfile
|-- APIGatewayConfig.yml

/HISPatientService
|-- /src
|-- /Controllers
|-- PatientController.cs
|-- /Services
|-- PatientService.cs
|-- /Repositories
|-- PatientRepository.cs
|-- /Models
|-- Patient.cs
|-- HISPatientService.csproj
|-- Startup.cs
|-- appsettings.json
|-- Dockerfile
|-- HISPatientServiceConfig.yml

/HISAppointmentService
|-- /src
|-- /Controllers
|-- AppointmentController.cs
|-- /Services
|-- AppointmentService.cs
|-- /Repositories
|-- AppointmentRepository.cs
|-- /Models
|-- Appointment.cs
|-- HISAppointmentService.csproj
|-- Startup.cs
|-- appsettings.json
|-- Dockerfile
|-- HISAppointmentServiceConfig.yml

/HISEHRService
|-- /src
|-- /Controllers
|-- EHRController.cs
|-- /Services
|-- EHRService.cs
|-- /Repositories
|-- EHRRepository.cs
|-- /Models
|-- EHR.cs
|-- HISEHRService.csproj
|-- Startup.cs
|-- appsettings.json
|-- Dockerfile
|-- HISEHRServiceConfig.yml

/HISOPDService
|-- /src
|-- /Controllers
|-- OPDController.cs
|-- /Services
|-- OPDService.cs
|-- /Repositories
|-- OPDRepository.cs
|-- /Models
|-- OPD.cs
|-- HISOPDService.csproj
|-- Startup.cs
|-- appsettings.json
|-- Dockerfile
|-- HISOPDServiceConfig.yml

/HISIPDService
|-- /src
|-- /Controllers
|-- IPDController.cs
|-- /Services
|-- IPDService.cs
|-- /Repositories
|-- IPDRepository.cs
|-- /Models
|-- IPD.cs
|-- HISIPDService.csproj
|-- Startup.cs
|-- appsettings.json
|-- Dockerfile
|-- HISIPDServiceConfig.yml

... (similar structure for other microservices)

/docker-compose.yml
/README.md
/.gitignore
/.env
