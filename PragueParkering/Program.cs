using Spectre.Console;


namespace PragueParkering
{
    internal class Program
    {
        static string[] ParkeringsGaragePlatser = new string[100]; // en lista av strängar som innehåller all fordons-data.
        static string[] fordonsVal = { "Bil", "MC" }; //Tillåtna fordon
        static Random random = new Random(); //en enkel random som används för test metoder
        static void Main(string[] args) // startar main menyn och skapar en ny markdown varje start
        {
            SkapaTommaFiler();
            MainMeny();
        }

        ///////////////////////////  MENY METODER  /////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////

        static void MainMeny() // Huvudmeny, enkelt ock går vidare till andra huvudmeny
        {
            while (true)
            {
                Console.Clear();
                var menyVal = PromptSelectionMedVal("[bold underline]Välkommen till Garaget, Vad vill du göra?[/]", ["Lediga Parkeringsplatser", "Boka nytt fordon", "Hämta ut fordon", "Flytta Fordon", "Admin och tester", "Avsluta"]);

                switch (menyVal)
                {
                    case "Lediga Parkeringsplatser":
                        ParkeringsMeny();
                        break;
                    case "Boka nytt fordon":
                        BokaFordonsMeny();
                        break;
                    case "Hämta ut fordon":
                        UthämtningsMeny();
                        break;
                    case "Flytta Fordon":
                        FlyttaFordonsMeny();
                        break;
                    case "Admin och tester":
                        AdminMeny();
                        break;
                    case "Avsluta":
                        Environment.Exit(0);
                        break;
                }
            }
        }
        static void ParkeringsMeny() //sub-meny gjord för att visa ett visuelt parkeringshus
        {
            ParkingGrid();
            if (Console.ReadKey(true).Key != ConsoleKey.Enter)
                return;

            Console.Clear();
            return;
        }
        static void BokaFordonsMeny() // sub-meny gjord för att hantera fordonsbokning
        {
            string fordon = VäljFordon();
            if (fordon == "Tillbaka")
            {
                Console.Clear();
                return;
            }
            string register = RegisterVald();
            if (string.IsNullOrEmpty(register))
            {
                Console.Clear();
                return;
            }
            string tid = DateTime.Now.ToString("MMMM/dddd/HH:mm").ToUpper();
            Console.Clear();
            AnsiConsole.MarkupLine("[bold]Din parkering är registrerad från nuvarande tidspunkt " + tid + " framtills avslut eller stänging [/]\n Tryck valfri knapp för att fortsätta\n");
            int plats = 0;
            int pPlats = 0;
            switch (PromptSelectionMedVal("Vill du välja en egen parkering eller ha en automatiskt tilldelad?", ["Välj själv", "Automatiskt tilldelad"]))
            {
                case "Välj själv":
                    ParkingGrid();
                    Console.WriteLine("Röda = upptaget, grön = ledig, gul = finns plats för en extra MC.\nAnge önskad plats.");
                    while (!int.TryParse(Console.ReadLine(), out plats) || plats < 1 || plats > 100)
                        Console.WriteLine("Fel inmatning eller ogiltig plats, försök igen");
                    plats = HittaParkering(plats, fordon);
                    break;
                case "Automatiskt tilldelad":
                    plats = HittaParkering(fordon);
                    break;

            }
            if (plats > 100)
            {
                pPlats = plats - 1000;
            }
            else pPlats = plats;
            Console.Clear();
            string slutVal = PromptSelectionMedVal($"[bold underline]Din parkerings plats blir {(pPlats)}[/]\n\nDitt fordons info är:\nFordon: \t{fordon}\nRegister: \t{register}\nStart tid: \t{tid}\nStämmer det?", ["Ja", "Nej"]);
            switch (slutVal)
            {
                case "Ja":
                    Console.WriteLine("Ett kvitto skrivs ut!");
                    SkrivKvitto(pPlats, fordon, register, tid);
                    NyttFordonIArray(plats, fordon, register, tid);
                    break;
                case "Nej":
                    break;

            }
        }
        static void UthämtningsMeny()// Gjord hämta ut/ta bort fordon om dom identifieras rätt.
        {
            Console.WriteLine("Här kan du hämta ut ditt fordon\n Vänligen identifiera ditt fordon\n");
            Console.WriteLine("Vad är ditt register?");
            string reg = Console.ReadLine().ToUpper();
            int index = Array.FindIndex(ParkeringsGaragePlatser, p => p != null && p.Contains(reg));
            if (index == -1)
            {
                Console.WriteLine("Fel register, tryck valfri knapp för att fortsätta");
                Console.ReadKey(true);
                return;
            }
            else
            {
                if (PreSplit(ParkeringsGaragePlatser[index], "fordon") == "Bil")
                {
                    ParkeringsGaragePlatser[index] = null;
                }
                else
                {
                    if (ParkeringsGaragePlatser[index].Contains('|'))
                    {
                        string fordon = PreSplit(ParkeringsGaragePlatser[index], "fordon");
                        string tid = PreSplit(ParkeringsGaragePlatser[index], "tid på två mc");
                        string tvåMc = PreSplit(ParkeringsGaragePlatser[index], "register två mc");

                        string[] deladMc = tvåMc.Split('|');
                        string[] deladTid = tid.Split('|');
                        ParkeringsGaragePlatser[index] = null;
                        if (deladMc[0].Contains(reg))
                        {
                            NyttFordonIArray(index + 1, fordon, deladMc[1], deladTid[1]);
                        }
                        else if(deladMc[0].Contains(reg))
                        {
                            NyttFordonIArray(index + 1, fordon, deladMc[0], deladTid[0]);
                        }
                    }
                    else ParkeringsGaragePlatser[index] = null;
                }
            }
            Console.WriteLine("Fordon uttaget!");
            Console.ReadKey(true);
            return;

        }
        static void AdminMeny()//sub-meny gjord för att kunna göra olika tester eller admin-saker
        {
            string val = PromptSelectionMedVal("[bold]Testmeny här kan du göra lite tester[/]", ["Skapa testfordon", "Flytta fordon", "Töm huset", "Stängnings Meny", "Kolla fordons Meny", "Tillbaka"]);
            switch (val)
            {
                case "Skapa testfordon":
                    {
                        string s = PromptSelectionMedVal("Vilken typ av fordon?", [fordonsVal[0], fordonsVal[1], "Tillbaka"]);
                        SlumpMässig(s);
                    }
                    break;
                case "Flytta fordon":
                    FlyttaFordonsMeny();
                    break;
                case "Töm huset":
                    Array.Clear(ParkeringsGaragePlatser, 0, ParkeringsGaragePlatser.Length);
                    break;
                case "Stängnings Meny":
                    StängningsMeny();
                    break;
                case "Kolla fordons Meny":
                    KollaFordonMeny();
                    break;
                case "Tillbaka":
                    break;

            }
        }
        static void FlyttaFordonsMeny()//en meny för att flytta fordon
        {
            switch (PromptSelectionMedVal("Här kan du flytta fordon från plats till plats", ["Flytta fordon", "Tillbaka"]))
            {
                case "Flytta fordon":
                    ParkingGrid();
                    Console.WriteLine("Vilket fordon vill du flytta?");
                    int val1;
                    while (!int.TryParse(Console.ReadLine(), out val1) || val1 < 1 || val1 > 100)
                        Console.WriteLine("Felaktig plats, ange plats mellan 1-100");

                    string fordon = HämtaFordonOmDetStämmer(val1);

                    Console.Clear();
                    ParkingGrid();
                    Console.WriteLine("Vart vill du flytta");
                    int val2;
                    while (!int.TryParse(Console.ReadLine(), out val2) || val2 < 1 || val2 > 100)
                        Console.WriteLine("Felaktig plats, ange plats mellan 1-100");
                    if (PreSplit(fordon, "fordon") == "MC")
                    {
                        string optimeradMC;
                        if (ParkeringsGaragePlatser[val2 - 1] != null && PreSplit(ParkeringsGaragePlatser[val2 - 1], "fordon") == "MC" && !ParkeringsGaragePlatser[val2 - 1].Contains("|"))
                        {
                            optimeradMC = Optimering(ParkeringsGaragePlatser[val1 - 1], val2);
                            FlyttaMellanTvåPlatser(val1, val2, optimeradMC);
                        }
                        else if (ParkeringsGaragePlatser[val2 - 1] == null)
                            FlyttaMellanTvåPlatser(val1, val2, fordon);
                        else Console.WriteLine("Platsen är full");
                        break;
                    }
                    if (PreSplit(fordon, "fordon") == "Bil")
                    {
                        if (ParkeringsGaragePlatser[val2 - 1] == null)
                            FlyttaMellanTvåPlatser(val1, val2, fordon);
                        break;
                    }
                    break;
                case "Tillbaka":
                    return;
            }


        }
        static void KollaFordonMeny() // meny för att kolla fordon
        {
            ParkingGrid();
            while (true)
            {

                string val = PromptSelectionMedVal("Vill du kolla ett fordon?", ["Ja", "Nej"]);
                switch (val)
                {
                    case "Ja":

                        Console.WriteLine("Vilket fordon vill du kolla?\nAnge en plats mellan 1-100 för att kolla parkeringsplatsen");
                        int.TryParse(Console.ReadLine(), out int userinput);
                        if (userinput > 0 && userinput <= 100)
                        {
                            Console.WriteLine(ParkeringsGaragePlatser[userinput - 1]);
                        }
                        break;
                    case "Nej":
                        Console.Clear();
                        return;
                }
            }
        }
        static void StängningsMeny()//en meny för att stänga parkeringshuset. inte automatisk
        {
            ParkingGrid();
            string val = PromptSelectionMedVal("Här kan du stänga Parkerings Huset", ["Stäng parkerings huset", "Tillbaka"]);
            switch (val)
            {
                case "Stäng parkerings huset":
                    {
                        if (AntalPlatserKvar("null") == 100)
                        {
                            Console.WriteLine("ParkeringsHuset är tomt, tryck valfri knapp för att fortsätta");
                            Console.ReadKey(true);
                            Console.Clear();
                            AdminMeny();

                        }
                        else
                        {
                            foreach (string s in ParkeringsGaragePlatser)
                                MarkDownNoter(s, "stängning");

                        }
                    }
                    break;
                case "Tillbaka":
                    break;
            }

        }

        ///////////////////////////// FORDONS METODER //////////////////////////////
        ///////////////////////////////////////////////////////////////////////////
        static void NyttFordonIArray(int plats, string fordon, string register, string tid) // Samlar fordons-information och skickar fordon till array
        {
            if (plats > 100)
            {
                plats -= 1000; // +1000 identifierar att optimering krävs
                int platsMc = plats;
                string NyttMc = Optimering(platsMc, fordon, register, tid);
                ParkeringsGaragePlatser[plats - 1] = NyttMc;
                Console.WriteLine();

            }
            else if (plats != -1)
            {

                string nyttFordon = StringJoiner(plats, fordon, register, tid);
                ParkeringsGaragePlatser[plats - 1] = nyttFordon;
                Console.WriteLine();
            }
            else throw new Exception("NÅGOT GICK FEL I :BOKANYTTFORDON");
            return;

        }
        static string Optimering(int platsMC, string fordon, string register, string tid)// varje plats kan ta 2st mc'fullString eller 1 bil,
                                                                                         // denna ska optimera mcs och lägga dom på samma.
        {
            string register1 = PreSplit
                 (HämtaFordonOmDetStämmer(platsMC), "register");

            string tid1 = PreSplit
                 (HämtaFordonOmDetStämmer(platsMC), "tid");

            string registerTvåMc = string
                 .Join("|", register1, register);

            string tidTvåMc = string
                 .Join("|", tid1, tid);

            MarkDownNoter(StringJoiner(platsMC, fordon, register, tid), "flytta mc");

            return string.Join("#", $"Plats {platsMC - 1}", fordon, registerTvåMc, tidTvåMc);

        }
        static string Optimering(string heltfordon, int plats)// varje plats kan ta 2st mc'fullString eller 1 bil denna ska optimera mcs och lägga dom på samma.
        {
            return string.Join("|", heltfordon, ParkeringsGaragePlatser[plats - 1]);
        }
        static string RegisterVald()// Return ett register OM den fyllar alla krav.
        {
            while (true)
            {
                Console.Clear();

                string register = AnsiConsole.Prompt(new TextPrompt<string>("[bold]Ange register eller lämna tom för att backa\nSkriv hela registret utan specialtecken eller mellanslag\nEXEMPEL: ABC123\n[/]")
                .AllowEmpty()
                .Validate(r =>
                {

                    if (r.All(char.IsLetterOrDigit) && (r.Length <= 10))
                        return ValidationResult.Success();

                    return ValidationResult.Error("[red bold]Max 10 tecken, bara siffror och bokstäver[/]");
                }));
                return register.ToUpper().Trim();
            }

        }
        static string VäljFordon() // väljer ett fordon från FordonsVal
        {
            return PromptSelectionMedVal("Välj typ av fordon", [fordonsVal[0], fordonsVal[1]]);
        }

        ///////////////////////////// ÖVRIGA METODER //////////////////////////////
        //////////////////////////////////////////////////////////////////////////

        static string PromptSelectionMedVal(string titel, params string[] val)// en selectionsprompts skapare som ska göra koden renare.
        {
            return AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title(titel)
                .AddChoices(val));
        }
        static void ParkingGrid() // En visuell representation av parkeringshuset
        {
            for (int i = 0; i < ParkeringsGaragePlatser.Length; i++)
            {

                if (ParkeringsGaragePlatser[i] == null)
                    AnsiConsole.Markup($"[green]|{i + 1,-3}    |[/]");

                else if (ParkeringsGaragePlatser[i].Contains("Bil"))
                    AnsiConsole.Markup($"[red]|{i + 1,-3} Bil|[/]");

                else if (ParkeringsGaragePlatser[i] != null)
                {
                    if (ParkeringsGaragePlatser[i].Contains("|"))
                    {
                        AnsiConsole.Markup($"[red]|{i + 1,-3} M/M|[/]");
                    }
                    else if (ParkeringsGaragePlatser[i].Contains("MC") && !ParkeringsGaragePlatser[i].Contains("|"))
                        AnsiConsole.Markup($"[yellow]| {i + 1,-3}M/0|[/]");
                }


                if ((i + 1) % 10 == 0)
                    AnsiConsole.WriteLine("");
                if ((i + 1) % 20 == 0)
                    AnsiConsole.WriteLine("\n");
            }
            AnsiConsole.MarkupLine("Här visas det aktiva parkeringshuset, valfri knapp för att återvända.\n");
        }
        static string StringJoiner(int plats, string fordon, string register, string? tid)//en string joiner som sätter ihop all parametrar till en string
        {
            if (plats != -1)
                return string.Join("#", $"Plats {plats}", fordon, register, tid);
            else return string.Join("#", fordon, register, tid);

        }
        static string HelaFordonEllerSplittadDel(string fullString, string del) //Tar ett en hel sträng för ett komplett fordon och delar upp det sen returnerar det användaren vill ha.
        {
            string[] delar = fullString.Split('#');
            switch (del.ToLower())
            {
                case "tid på två mc":
                    return delar[3];
                case "register två mc":
                    return delar[2];
                case "plats":
                    return delar[0];
                case "fordon":
                    return delar[1];
                case "register":
                    if (delar[1] == "MC" && delar.Contains("|"))
                    {
                        string val = PromptSelectionMedVal("Parkeringen har 2st MC'xPlats parkerade, vill du ha register #1/#2?", ["#1", "#2", "tillbaka"]);
                        string[] mc2 = fullString.Split('|');
                        switch (val.ToLower())
                        {
                            case "#1":
                                return mc2[0];
                            case "#2":
                                return mc2[1];
                            case "tillbaka":
                                return string.Empty;
                        }
                    }
                    return delar[2];
                case "tid":
                    return delar[3];
                case "plats nummer":
                    {
                        string s = delar[0];
                        string nummer = s.Split(" ")[1];
                        return nummer;
                    }
            }
            return "FEL I FORDONSPLIT";
        }
        static int HittaParkering(int plats, string fordon)// Kollar på parkering åt bil eller mc, mc som ska kombineras får +1000 för identifiering 
        {
            int platser = AntalPlatserKvar($"{fordon}");
            if (platser > 0)
            {
                while (true)
                {
                    string xPlats = ParkeringsGaragePlatser[plats - 1];

                    if (xPlats == null)
                        return plats;

                    if (fordon == "MC" && PreSplit(xPlats, "fordon") == "MC" && !xPlats.Contains("|"))
                        return plats + 1000;

                    string val = PromptSelectionMedVal("Platsen är upptagen, vill du välja en ny plats?", ["Ja", "Nej"]);
                    if (val == "Ja")
                    {
                        Console.Clear();
                        ParkingGrid();
                        Console.WriteLine("Välj en tillgänglig plats");
                        while (!int.TryParse(Console.ReadLine(), out plats) || plats < 1 || plats > 100)
                            Console.WriteLine("Fel inmatning eller ogiltig plats, försök igen");
                    }
                    if (val == "Nej")
                        return -1;
                }
            }
            Console.WriteLine("Parkeringshuset är fullt, tryck valfri knapp för att gå tillbaka");
            Console.ReadKey(true);
            return -1;
        }
        static int HittaParkering(string fordon)// OVERLOAD gjord för automatisk parkeringsplats, mc som ska kombineras får + 1000 för identifiering
        {
            int platser = AntalPlatserKvar($"{fordon}");
            if (platser > 0)
            {
                int plats = 0;
                if (fordon == "MC")
                {
                    int mcPlats = Array.FindIndex(ParkeringsGaragePlatser, p => p != null && PreSplit(p, "fordon") == "MC" && !p.Contains("|"));
                    if (mcPlats != -1)
                    {
                        return mcPlats + 1 + 1000;
                    }
                }
                plats = Array.FindIndex(ParkeringsGaragePlatser, p => p == null);

                return plats + 1;
            }
            Console.WriteLine("Parkeringshuset är fullt, tryck valfri knapp för att gå tillbaka");
            Console.ReadKey(true);
            return -1;
        }
        static int AntalPlatserKvar(string typ)// Returnerar antalet kvar av typ du lägger in, Bil, Mc, Null,
        {
            int m = 0;
            int n = 0;
            foreach (string item in ParkeringsGaragePlatser)
            {
                if (string.IsNullOrEmpty(item))
                    n++;
                else if (item.Contains("MC") && !(item.Contains("|")))
                    m++;
            }
            switch (typ.ToLower())
            {
                case "mc":
                    return (n * 2) + m;
                case "bil":
                    return n;
                case "null":
                    return n;
                default:
                    return 0;

            }
        }
        static string HämtaFordonOmDetStämmer(int plats) // Hämta fordon via plats# 
        {
            if (ParkeringsGaragePlatser[plats - 1] == null)
                return string.Empty;
            else if (ParkeringsGaragePlatser[plats - 1].Contains("|"))
            {
                return PreSplit(ParkeringsGaragePlatser[plats - 1], "separera mc");
            }
            else return ParkeringsGaragePlatser[plats - 1];

        }
        static void SkrivKvitto(int platsNummer, string fordon, string register, string tid)//Skriver ett kvitto till Kvitton.md
        {
            string kvitto =
                $"## Parkeringskvitto\n\n" +
                $"- Plats: {platsNummer}\n" +
                $"- Fordon: {fordon}\n" +
                $"- Registreringsnummer:{register}\n" +
                $"- Starttid: {tid:yyyy-MM-dd HH:mm}\n\n" +
                $"---\n\n";

            string filePath2 = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Kvitton.md");
            File.AppendAllText(filePath2, kvitto);
        }
        static void MarkDownNoter(string order, string händelse)
        {
            if (händelse == "flytta mc")
            {


                string[] delar = order.Split('#');
                string markdown = $"- OPTIMERA MC FORDON TILL: {delar[0]} Fordon: {delar[1]} | Register: {delar[2]} | Tid: {delar[3]}\nFORDON FLYTTAT\n";

                // Spara i projektmappen
                string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "ArbetsOrdrar.md");
                File.AppendAllText(filePath, markdown);
            }
            else if (händelse == "stängning")
            {
                string[] delar = order.Split('#');
                string markdown = $"- HUSET STÄNGER FÖLJANDE FORDON FLYTTAS: {delar[0]} Fordon: {delar[1]} | Register: {delar[2]} | Tid: {delar[3]}\nFORDON FLYTTAT\n";

                // Spara i projektmappen
                string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "ArbetsOrdrar.md");
                File.AppendAllText(filePath, markdown);
            }


        }// skickar Optimerings ordrar till en Markdown
        static void SkapaTommaFiler()
        {
            string filePath1 = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "ArbetsOrdrar.md");
            File.WriteAllText(filePath1, "");

            string filePath2 = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Kvitton.md");
            File.WriteAllText(filePath2, "");
        } // Skapar en tom fil varje gång programmet startar för Markdown
        static string PreSplit(string fullString, string del) // gjordes för att hitta fel under krasch men jag tror den är onödig.
        {
            try
            {
                string a = HelaFordonEllerSplittadDel(fullString, del);
                return a;
            }
            catch
            {
                return string.Empty;
            }
        }
        static void FlyttaMellanTvåPlatser(int plats1, int plats2, string fordon)//flyttar ett fordon till en ny plats
        {
            ParkeringsGaragePlatser[plats2 - 1] = fordon;
            ParkeringsGaragePlatser[plats1 - 1] = null;
        }

        ///////////////////////////// RANDOM METODER //////////////////////////////
        //////////////////////////////////////////////////////////////////////////

        static void SlumpMässig(string fordonTyp)//skapar ett antal slumpmässiga Fordon med max 10st symboler, random tid mellan 01:00 & nu-tid
        {
            int y;

            Console.WriteLine($"Det finns plats för: {AntalPlatserKvar(fordonTyp)} st {fordonTyp}\nSkriver du fler än tillåtna så skickas du tillbaka i menyn");

            int.TryParse(Console.ReadLine(), out y);

            if (y <= 0 || y >= AntalPlatserKvar(fordonTyp))
            {
                Console.Clear();
                Console.WriteLine("Du försökte skapa för många fordon");
                AdminMeny();
            }
            while (y > 0)
            {
                string? randomFordon = fordonTyp ?? fordonsVal[random.Next()];
                string randomTime = RandomTime();
                string randomRegister = RandomReg();
                int plats = HittaParkering(randomFordon);

                NyttFordonIArray(plats, randomFordon, randomRegister, randomTime);
                y--;
            }
        }
        static string RandomReg()//Skapar ett random register på 10symboler som sen används i SlumpMässig
        {
            string tecken = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ0123456789";
            char[] reg = new char[10];
            for (int i = 0; i < 10; i++)
                reg[i] = tecken[random.Next(tecken.Length)];
            return new string(reg);
        }
        static string RandomTime() // Skapar en random tid idag mellan 01:00 & Nu-Tid som sen används i SlumpMässig
        {
            DateTime start = DateTime.Today.AddHours(1);
            DateTime slut = DateTime.Now;
            int tid = random.Next(0, (int)(slut - start).TotalMinutes);
            DateTime minuter = start.AddMinutes(tid);
            return minuter.ToString("MMMM/dddd/HH:mm");
        }



    }
}
