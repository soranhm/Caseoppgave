# Caseoppgave

Ved overføring av en fil til en Azure Blob, skal det trigges (Azure Storage Blob Trigger) en Azure Function App. Denne skal lese og videresende innholdet i filen til Azure Service Bus.

# Nødvenidge programmvarer

- Service Bus Explorer
- Microsoft Azure Storage Explorer
- Microsoft Azure Storage Emulator
- Visual Studio Code / Visual Studio Community Edition (2019 version)
- .Net Core 3.1 SDK
- Azure Function Tools
- Azure-konto

# Kjøring

Caseoppgaven kan kjøres ved å klone over repositoriet på egen pc og åpne opp prosjektet `Caseoppgave.sln` og kjøre filen `Function1.cs` som vil åpne opp en Azure Function terminal. DEt er nødvenidg å kjøre et lokalt storgae med "Microsoft Azure Storage Emulator" og og lage en blob container i local-1 som med navnet `caseoppgave`!.

## Gjennomgang

- Det første som kommer opp etter kjøring av filen `Function1.cs` er:
  ![](https://github.com/soranhm/Caseoppgave/blob/master/images/run1.PNG?raw=true)

Dette betyr at alt er oppe og går.

- Neste steg er å dra en .txt fil på formen :

```
Fornavn;Etternavn;Tittel
Smart;Ness;Sjef
Arne;Brimi;Kokk
```

over til Blob containeren `caseoppgave`.
![](https://github.com/soranhm/Caseoppgave/blob/master/images/run2.PNG?raw=true)

- Ved overføring av filen, vil `BlobTrigger` slå inn:
  ![](https://github.com/soranhm/Caseoppgave/blob/master/images/run3.PNG?raw=true)

- Dermed har informationen i .txt filen blitt behandlet og skrevet om til .json format, før sent videre til Service Bus Explorer som vil sende ut 2 meldinger som består av Personene spesifisert i .txr filen:
  ![](https://github.com/soranhm/Caseoppgave/blob/master/images/run4.PNG?raw=true)

- Service Bus Explorer er koblet med Azure-kontoen. Dette kan visualiserers ved å se antall aktive meldinger i Azure:
  ![](https://github.com/soranhm/Caseoppgave/blob/master/images/run5.PNG?raw=true)
