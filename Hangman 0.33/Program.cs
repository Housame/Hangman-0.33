using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman_0._33
{
    class Program
    {
        #region Klassvariabler
        static Player player;
        static string chosenLevel;
        static string hiddenWord;
        static Char[] maskedWord;
        static string usedCharacter;
        static char playerGuess;
        static int playerLives = 3;
        #endregion
        static void Main(string[] args)
        {

            Name();

        }

        static void Name()
        {
            Gui();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n Skriv in ditt namn!: ");
            Console.ResetColor();
            string input;
            input = Console.ReadLine();
            if (input.Length > 1 && input.Length < 26)
            {
                player = new Player(input);
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\n                               Hej!{0} ! Nu kommer du till HuvudMeny                                                 ", player.name.ToUpper());
                Console.ResetColor();
                Timer(1.5);
            }
            else
            {
                input = null;
                Console.WriteLine("Var vänlig och skriv in ett giltigt namn");
                Timer(1);
                Console.Clear();
                Name();
            }

            StartMenu();
        }

        private static void Gui()
        {

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("================_========_========_===========_=====_==_= _=====_===========_========_========_===========_===============_==_=================_==_==_==_=======");
            Console.WriteLine("|| (_)         (_)     _(_)_     (_) _       (_) _ (_)(_)(_) _ (_) _     _ (_)     _(_)_     (_) _       (_)           _ (_)(_) _            _(_)(_)(_)(_)_   ||");
            Console.WriteLine("|| (_)         (_)   _(_) (_)_   (_)(_)_     (_)(_)         (_)(_)(_)   (_)(_)   _(_) (_)_   (_)(_)_     (_)          (_)      (_)          (_)          (_)  ||");
            Console.WriteLine("|| (_) _  _  _ (_) _(_)     (_)_ (_)  (_)_   (_)(_)    _  _  _ (_) (_)_(_) (_) _(_)     (_)_ (_)  (_)_   (_)         (_)        (_)                  _  _(_)  ||");
            Console.WriteLine("|| (_)(_)(_)(_)(_)(_) _  _  _ (_)(_)    (_)_ (_)(_)   (_)(_)(_)(_)   (_)   (_)(_) _  _  _ (_)(_)    (_)_ (_)         (_)        (_)                 (_)(_)_   ||");
            Console.WriteLine("|| (_)         (_)(_)(_)(_)(_)(_)(_)      (_)(_)(_)         (_)(_)         (_)(_)(_)(_)(_)(_)(_)      (_)(_)         (_)        (_)  _  _    _           (_)  ||");
            Console.WriteLine("|| (_)         (_)(_)         (_)(_)         (_)(_) _  _  _ (_)(_)         (_)(_)         (_)(_)         (_)          (_) _  _ (_)  (_)(_)  (_)_  _  _  _(_)  ||");
            Console.WriteLine("|| (_)         (_)(_)         (_)(_)         (_)   (_)(_)(_)(_)(_)         (_)(_)         (_)(_)         (_)             (_)(_)     (_)(_)    (_)(_)(_)(_)    ||");
            Console.WriteLine("================================================================================================================================================================");
            Timer(1.5);
        }

        static void StartMenu()
        {

            while (true)
            {
                int caseSwitch;
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("                       HuvudMeny                                                                                        ");
                Console.ResetColor();

                Console.WriteLine("1. För att starta spelet, tryck 1");
                Console.WriteLine("2. För att läsa instruktionerna, tryck 2");
                string input = Console.ReadLine();

                if (input == "1" || input == "2")

                    caseSwitch = int.Parse(input);

                else

                    caseSwitch = 0;

                switch (caseSwitch)
                {
                    case 1:
                        Console.Clear();
                        StartGame();
                        return;
                    case 2:
                        Console.Clear();
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Clear();
                        HowTo();
                        return;
                    default:
                        Console.WriteLine("Vänlligen skriv in ett giltigt val 1 eller 2");
                        Timer(1);
                        Console.Clear();
                        break;
                }

            }

        }

        private static void HowTo()
        {

            Console.WriteLine("How to!");
            Console.WriteLine("Klura ut det dolda ordet en bokstav i taget. ");
            Console.WriteLine("Lyckas du klura ut ordet får du fortsätta med nästa,");
            Console.WriteLine("misslyckas du däremot dras ett liv");
            Console.WriteLine("när dina 5 liv  är slut är det game over...");
            Console.WriteLine("\n Vill du återgå till HuvudMeny tryck 1, Vill du börja spela Tryck 2");

            string input = Console.ReadLine();
            if (input == "1" || input == "2")
            {
                int caseSwitch = int.Parse(input);
                switch (caseSwitch)
                {
                    case 1:
                        Console.WriteLine("Nu kommer till till HuvuMenyn!!!");
                        Timer(1.2);
                        Console.Clear();
                        StartMenu();
                        break;
                    case 2:
                        Console.WriteLine("Nu ´Börjar spelet, Lycka till!!!");
                        Timer(1.2);
                        Console.Clear();
                        StartGame();
                        break;
                }


            }
            else
            {
                input = null;
                Console.WriteLine("Var vänlig och ange ett giltigt val 1 eller 2");
                Timer(1);
                Console.Clear();
                HowTo();
            }
        }

        private static void StartGame()
        {
            Level();
            WordGenerator();
            GameLoop();
        }

        private static void WordGenerator()
        {
            //"APA", "TROLL", "STEG", "BEBIS", "MAN", "KVINNA", "MORGON", "FEST", "SOL",
            string[] hiddenWordEasy = new string[] {  "BAD", "BAD", "BAD", "BAD", "BAD", "BAD", "BAD", "BAD", "BAD", "BAD", "BAD", };
            string[] hiddenWordNormal = new string[] { "HÖGER", "VATTEN", "STJÄRNA", "DATOR", "LASTBIL", "BYXOR", "VÄSKA", "PRINSESSA", "TRAFIK", "ZLATAN" };
            string[] hiddenWordHard = new string[] { "PROGRAMMERING", "SUNDBYBERG", "CIGARETTER", "POKEMON", "HANGMAN", "FLYGPLAN", "DIAGNOS", "SJUKDOM", "SYNONYM", "SJUKSKÖTERSKA" };

            Random rnd = new Random();
            int card = rnd.Next(11);
            switch (chosenLevel)
            {
                case "Easy":
                    hiddenWord = hiddenWordEasy[card];
                    break;
                case "Normal":
                    hiddenWord = hiddenWordNormal[card];
                    break;
                case "Hard":
                    hiddenWord = hiddenWordHard[card];
                    break;
                default:
                    hiddenWord = null;
                    break;
            }

        }

        private static void Level()
        {
            Console.WriteLine("Ange vilken svårighetsgrad vill du köra:");
            Console.WriteLine("1. Lätt");
            Console.WriteLine("2. Mellan");
            Console.WriteLine("3. Svår");
            string input = Console.ReadLine();

            if (input == "1" || input == "2" || input == "3")
            {
                int caseSwitch = int.Parse(input);
                switch (caseSwitch)
                {
                    case 1:
                        Console.WriteLine("Du valde Lätt!! Spelet böjar strax!!");
                        chosenLevel = "Easy";
                        Timer(1);
                        Console.Clear();
                        GameLoop();
                        break;
                    case 2:
                        Console.WriteLine("Du valde Mellan!! Spelet böjar strax!!");
                        chosenLevel = "Normal";
                        Timer(1);
                        Console.Clear();
                        GameLoop();
                        break;
                    case 3:
                        Console.WriteLine("Du valde Svår!! Spelet böjar strax!!");
                        chosenLevel = "Hard";
                        Timer(1);
                        Console.Clear();
                        GameLoop();
                        break;

                }

            }
        }

        private static void GameLoop()
        {
            WordGenerator();
            while (true)
            {
                

                Guess();
                CompareWord();
                if (CompareWord() == true) return;
                

            }
        }

        private static bool Guess()
        {
            
            while (true)
            {
                maskedWord = new char[hiddenWord.Length];
                for (int i = 0; i < hiddenWord.Length; i++)
                {
                    maskedWord[i] = '_';
                    Console.Write(" " + maskedWord[i]);

                }
                Console.WriteLine();
                Console.WriteLine("Ange en bokstav, tack!: ");
                string input = Console.ReadLine();
                if (!(input.Length == 1))
                {
                    Console.WriteLine("Var vänlig och mata in en Bokstav, tack");
                    input = null;
                    Timer(1);

                }
                else
                {
                    bool result = input.All(Char.IsLetter);
                    
                    if (result)
                    {
                        input = input.ToUpper();
                        playerGuess = Char.Parse(input);                      
                       Console.WriteLine("Du gissade på: {0} ",playerGuess);
                            
                        return false;
                    }

                    else
                    {
                        Console.WriteLine("Var vänlig och mata in en Bokstav, tack");
                        Timer(1);
                        Console.Clear();
                        return true;
                    }
                }
            }
        }

        private static void PlayerGuess()
        {

            Console.WriteLine("Varsågod och försök att avslöja ordet:");


        }

        private static bool CompareWord()
        {
            bool condition = false;
            int winCounter = 0;
            for (int i = 0; i < hiddenWord.Length; i++)
            {
                if (playerGuess == hiddenWord[i])
                {
                    maskedWord[i] = playerGuess;
                    condition = true;
                }
            }
            if (condition==true)
                {
                winCounter++;
                }
            else if (condition ==false)
            {
                playerLives--;
            }
            if (winCounter==hiddenWord.Length)
            { 
                Timer(1);
                Winner();
                return true;
            }
            else if (playerLives==0)
            {
                Timer(1);
                Loser();
                return true;
            }


            return false;
            
        }

        private static void Loser()
        {
            Timer(0.5);
            Console.Clear();
            Console.WriteLine("\n\nDödstraffet är förbjudet enligt FN:regler");
            Timer(3.5);
            Console.WriteLine("\n\nmen inte här för du visste att din gubbe hängs om du misslyckas");
            Timer(3);
            Console.WriteLine("\n");
            Console.WriteLine("  ___________.._______");
            Console.WriteLine("| .__________))______ |");
            Console.WriteLine("| | / /      ||");
            Console.WriteLine("| |/ /       ||");
            Console.WriteLine("| | /        ||.- ''.");
            Console.WriteLine("| |/         |/ _  \"");
            Console.WriteLine("| |          ||  `/,|");
            Console.WriteLine("| |         (\\`_.'");
            Console.WriteLine("| |         .-`--'.");
            Console.WriteLine("| |        / Y. .Y\"");
            Console.WriteLine("| |       // |   | \\");
            Console.WriteLine("| |      //  | . |  \\");
            Console.WriteLine("| |     ')   |   |   (`");
            Console.WriteLine("| |          || '||");
            Console.WriteLine("| |          || ||");
            Console.WriteLine("| |          || ||");
            Console.WriteLine("| |          || ||");
            Console.WriteLine("| |         / | | \"");
            Console.WriteLine("          | _`-' `-' |    |  ");
            Console.WriteLine("||||||||||||||||||||||||||||| |");
            Console.WriteLine("| ||||||||||||||||||||||||||| |");
            Console.WriteLine(": ::::::::::::::::::::::::::: :  ");
            Console.WriteLine(". ........................... .");
            Timer(1.5);
            BackToStart();
            
        }

        private static void Winner()
        {
            Console.Clear();
            Console.WriteLine("\n\nBra jobbat {0} !,Du avslöjade ordet och räddade gubben från att hängas!!", player.name);
            Timer(1);
            BackToStart();
        }
        
        private static void BackToStart()
        {
            Console.Clear();
            Console.WriteLine("Det gömda ordet är: " + hiddenWord);

            Console.WriteLine("Hoppas du hade kul{0}", player.name.ToUpper());
            Console.Write("Vill du spela om? Tryck 1");
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("        ");
            Console.ResetColor();
            Console.WriteLine("Vill du avsluta? Tryck 2");
            string input = Console.ReadLine();
            if (input == "1" || input == "2")
            {
                int caseSwitch = int.Parse(input);
                switch (caseSwitch)
                {
                    case 1:
                        Console.Clear();
                        NewOrNot();
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("Hoppas att spelet var bra, Välkommen åter{0}", player.name.ToUpper());
                        Timer(1.5);
                        Environment.Exit(0);
                        break;


                }
            }
            else
            {
                input = null;
                Console.WriteLine("Var vänlig och ange ett giltigt val 1 eller 2");
                Timer(1);
                Console.Clear();
                BackToStart();
            }


        }

        private static void NewOrNot()
        {
            Console.Write("Samma spelare? Tryck 1,");
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write("         ");
            Console.ResetColor();
            Console.WriteLine("Ny spelare? Tryck 2");
            string input = Console.ReadLine();
            if (input == "1" || input == "2")
            {
                int caseSwitch = int.Parse(input);
                switch (caseSwitch)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("Du kör en runda till {0}, Lycka Till!!", player.name.ToUpper());
                        StartMenu();
                        break;
                    case 2:
                        Console.Clear();
                        Name();
                        break;
                }
            }
            else
            {
                input = null;
                Console.WriteLine("Var vänlig och ange ett giltigt val 1 eller 2");
                Timer(1);
                Console.Clear();
                NewOrNot();
            }

        }

        public static void Timer(double seconds)//timer med input(double)
        {
            {
                double sec = seconds;
                var t = Task.Run(async delegate
                {
                    await Task.Delay(TimeSpan.FromSeconds(sec));
                    return;
                });
                t.Wait();

            }
        }
    }
}