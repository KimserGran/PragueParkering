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


## ----------------------------- ## PERSONLIG REFLEKTION
1.Jag började uppgiften med måtlig säkerhet och dag efter dag kom det fram mer självsäkerhet.
	började skriva metoder jag antog jag behöver och sen var det att börja, vilket jag började med arrayen.
	sen var det att börja bygga metoderna för att sätta in första fordonet.
	Jag kände mig väldigt stolt sen när jag fixade en visuelisering för parkerings huset, såg riktigt bra ut också.
	Personligen stötte jag på några få "väghinder" så att säga, jag fick lite stökig logik när jag skulle optimera eller parkera ibland.
	Utöver det så flöt kodningen på bra, jag tog lite extra tid på mig för att jag var överflödig inom projektet men det var mitt val,
	jag måste vara försiktig med det så att jag inte skapar en vana att bygga mer än vad som frågat.

2.Jag löste det i slutändan ganska bra för att jag kände mig mer och mer säker när jag skrev, jag kunde läsa min kod och förstå den bra.
	min kod gjorde i slutändan vad jag ville och nu kan man enkelt flytta runt menyer utan problem så det i sig självt är bevis på att allt funkar.

3.Det största problemet jag hade under denna uppgift var en blandning av logik och att skifta mellan plats (1-100) och index (0-99), 
	jag stötte på ett problem med att jag först ibland ersätte fordon som redan var parkerade, MC fordon jag hade samlade och optimerades inte.
	MEN, jag löste det!, i stunden är jag osäker vad själva felet var men är 100% säker på att det var ett logik fel, kan ett kombinerat fel i både Optimerings metoden samt HittaParkering metoden
	Jag löste det steg vis genom att skapa brytplatser och hålla koll på vart värden samt plats värde gick fel.

4.Som sagt var jag lite överflödig i mitt program för ett skoluppgift och gick ett steg längre,
	metoderna som behövdes för att lösa uppgiften finns alla med känner jag, men absolut kunde jag ha dubblat antalet metoder och separerat vad dom gör ännu mer. kände att jag hittade en ok balans

5.Om jag skulle göra om uppgiften igen skulle jag bygga på ett mycket mer stegvis sätt, gärna från antigen botten up eller toppen ner, båda funkar. 
	Jag fick lite A.D.D ibland under uppgiften när jag fick en idé och började bygga nya metoder, detta gjorde i början projektet lite stökigt.

6.Slutsats hemuppgift: en kul och väldigt intressant uppgift fokuserade på många olika delar vi gått igenom 
	visa saker var tuffare som när jag fastnade på logiken men generellt var det en väldigt balanserad uppgift.

7.Slutsats kurs: Kursen i helhet än så länge känns väldigt bra, men denna uppgift är egentligen en väldigt liten del av c# programmering












## ----------------------------- ## AI SUMMARY // BARA FÖR SKOJS SKULL!

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

