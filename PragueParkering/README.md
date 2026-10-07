## ----------------------------- ##Projektet Börjar
Jag började med att lära mig mer om specterconsole och vissa av dess metoder och klasser.
	specterconsol är ganska behändigt måste jag säga.

Jag skapade en meny med switch val och flera metoder för submenyer.

Jag testade att leka med en "Table" som kommer från specter väldigt intressant.
	valde dock att inte använda det för uppgiften

Började bygga många små metoder, jag försöker följer regeln att om jag behöver använda något mer än 1 gång
	så blir det en metod, jag skapade så många jag trodde jag behöver men kommer definitivt behöva fler.
####

## ----------------------------- ## Problem
Jag har ibland haft fel syntax eller inte riktigt kommit ihåg hur något funkar, då brukar jag bolla lite med AI,
	MEN jag kopierar ALDRIG från AI bara frågar om tips för syntax eller direktion
####

Annars har börjarn varit ganska straightforward.

## ----------------------------- ## Projektet fortsätter

jag skapade en StringJoiner metod och en HämtaOchSplitaFordon metod för att dela eller joina fordons delar.

Jag la till testmenyn
	Byggde lite olika tester för skoj det är inte ett krav på uppgiften men jag gillar dom.

Jag la till en split metod som hjälper mig att hämta ut data från arrayen
####

## ----------------------------- ## Problem
Jag har haft lite svårt att optimera MC's i mitt program, finns kanske lättare sätt men jag ska fortsätta försöka.

####


## ----------------------------- ## Projektet fortsätter

Jag skapade en visualisering för parkeringen,
	känns bra och ser bra ut. den visar grön färg för öppnade parkeringar eller rött för upptagna.

Gjorde om min optimering och ändrade insåg ett logiskt fel i min skapa testfordons meny, ska åtgärda.

Lyckades optimera MC's funkar bra.
####

## ----------------------------- ## Problem
Inga större problem
####

## ----------------------------- ## Projektet Fortsätter
Jag känner mig säkrare när jag skriver kod specielt lite mer komplicerad kod.

Skapade en metod för att lägga in arbetsordrar i en markdown, samt en metod för att rensa markdown
	när programmet startar om

byggde en metod för att också skriva kvitton.


## ----------------------------- ## Projektet KLART

Jag har gjort klart allt, nu ska jag snygga till lite.

rensade och fixade en metod för selection prompts.

allt ser bra ut.












## ----------------------------- ## AI SUMMARY

**PragueParkering** är ett console-baserat parkeringsgaraget-system byggt i .NET 10 med Spectre.Console för användargränssnittet. 

Systemet hanterar ett parkeringshus med 100 platser där användare kan:
- **Boka fordon** - Registrera bilar eller motorcyklar (MC) med registreringsnummer och tidsdata
- **Visa lediga platser** - Visuell representation av parkeringshuset med färgkodning (grön = ledig, röd = upptagen, gul = MC med plats för extra MC)
- **Hämta ut fordon** - Ta bort fordon från parkeringen genom registreringsnummer
- **Admin-funktioner** - Testverktyg för att skapa testfordon, flytta fordon mellan platser, och tömma huset

Systemets huvudfunktionalitet:
- Lagring av fordonsdata i en array (Plats, Fordonstyp, Register, Tidsstämpel)
- **Optimering för MC** - Två MC kan parkeras på samma plats (sammanslutna med `|` separator) 
- **Kvittohantering** - Genererar och sparar kvitton i Markdown-format
- **Registreringskontroll** - Validering att registreringsnummer följer format (max 10 tecken, endast bokstäver/siffror)
- **Menynavigering** - Användarflöde genom Spectre SelectionPrompt med alternativ för autom. eller manuel platsval

Arkitektur:
- Enklassig design med statiska metoder för menyer, validering, lagring och utskrift
- Använder Spectre.Console för interaktivt CLI-gränssnittet
- Markdown-loggning för arbetsordrar och systemöversi

