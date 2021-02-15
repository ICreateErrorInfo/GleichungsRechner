using System;
using System.Collections.Generic;
using System.Text;

namespace GleichungsRechner
{
    class Berechne
    {
        public static List<string> z = new List<string>();
        public static List<string> o = new List<string>();

        public static void parser(string input)
        {
            char[] inputCh;
            inputCh = input.ToCharArray();

            for (int i = 0; i < inputCh.Length; i++)
            {
                if (isOperator(inputCh[i]))
                {
                    o.Add(inputCh[i].ToString());
                }
                else if (isZahl(inputCh[i]))
                {
                    if(i + 1 < inputCh.Length)
                    {
                        if (isZahl(inputCh[i]) && isZahl(inputCh[i + 1]))
                        {
                            z.Add((Convert.ToDouble(inputCh[i].ToString()) * 10 + Convert.ToDouble(inputCh[i + 1].ToString())).ToString());
                            i += 1;
                        }
                        else
                        {
                            z.Add(inputCh[i].ToString());
                        }
                    }
                    else
                    {
                        z.Add(inputCh[i].ToString());
                    }
                }
            }
        }
        public static double Term(string input)
        {
            string[] inputZ;
            inputZ = input.Split(Operatoren, StringSplitOptions.RemoveEmptyEntries);  //initialize

            string[] inputOArr;
            inputOArr = input.Split(Zahlen, StringSplitOptions.RemoveEmptyEntries);   //initialize
            var inputO = new List<string>(inputOArr);

            double ergebnis = 0;

            if (inputOArr.Length == inputZ.Length)
            {
                double erstezahl = Convert.ToInt32(inputZ[0]);
                if (inputO[0] == "-")
                {
                    erstezahl = erstezahl - (erstezahl * 2);
                    inputZ[0] = erstezahl.ToString();
                    inputO.RemoveAt(0);
                    inputOArr = inputO.ToArray();
                }
                else
                {
                    inputO.RemoveAt(0);
                    inputOArr = inputO.ToArray();
                }
            } //vorzeichen erkenner 

            for (int x = 0; inputOArr.Length - 1 > x; x++)
            {
                double z = Convert.ToDouble(inputZ[x + 1]);
                if (inputOArr[x] == "-")
                {
                    z = z - (z * 2);
                    inputZ[x + 1] = z.ToString();
                }
                else if (inputOArr[x] == "+")
                {
                    z = z;
                }
            } //Vorzeichen erstellen

            ergebnis = PunktVorStrich(inputOArr, inputZ);

            return ergebnis;
        }

        public static bool isOperator(char input)
        {
            int i = 0;
            foreach(int element in Operatoren)
            {
                if(input == Operatoren[i])
                {
                    return true;
                }
                i++;
            }
            return false;
        } // check ob input is operator
        public static bool isZahl(char input)
        {
            int i = 0;
            foreach (int element in Zahlen)
            {
                if (input == Zahlen[i])
                {
                    return true;
                }
                i++;
            }
            return false;
        } // check ob input is zahl
        public static double PunktVorStrich(string[] Operator, string[] inputZ)
        {
            double ergebnis = 0;

            for (int x = 0; Operator.Length > x; x++)
            {
                if (Operator[x].Contains('^'))
                {
                    if (ergebnis == 0)
                    {
                        ergebnis = Convert.ToDouble(inputZ[x + 1]);
                    }

                    ergebnis = Berechner(Operator[x], ergebnis, inputZ[x]);
                }

            }
            for (int x = 0; Operator.Length > x; x++)
            {
                if (Operator[x].Contains('*'))
                {
                    if (ergebnis == 0)
                    {
                        ergebnis = Convert.ToDouble(inputZ[x + 1]);
                    }

                    ergebnis = Berechner(Operator[x], ergebnis, inputZ[x]);
                }

            }
            for (int x = 0; Operator.Length > x; x++)
            {
                if (Operator[x].Contains('/'))
                {
                    if (ergebnis == 0)
                    {
                        ergebnis = Convert.ToDouble(inputZ[x + 1]);
                    }

                    ergebnis = Berechner(Operator[x], ergebnis, inputZ[x + 1]);
                }

            }
            for (int x = 0; Operator.Length > x; x++)
            {
                if (Operator[x].Contains('+'))
                {
                    if (ergebnis == 0)
                    {
                        ergebnis = Convert.ToDouble(inputZ[x + 1]);
                    }

                    ergebnis = Berechner(Operator[x], ergebnis, inputZ[x]);
                }

            }
            for (int x = 0; Operator.Length > x; x++)
            {
                if (Operator[x].Contains('-'))
                {
                    if (ergebnis == 0)
                    {
                        ergebnis = Convert.ToDouble(inputZ[x + 1]);
                    }

                    ergebnis = Berechner(Operator[x], ergebnis, inputZ[x]);
                }

            }
            return ergebnis;
        } // punkt vor strich beachtung
        public static double Berechner(string Operator, double zahl2, string ergebnis1)
        {
            var ergebnis = Convert.ToDouble(ergebnis1);

            if (Operator == "^")
            {
                for (int x = 1; zahl2 > x; x++)
                {
                    ergebnis = ergebnis * ergebnis;
                }
            }
            else if (Operator == "*")
            {
                ergebnis = ergebnis * zahl2;
            }
            else if (Operator == "/")
            {
                ergebnis = ergebnis / zahl2;
            }
            else if (Operator == "+")
            {
                ergebnis = ergebnis + zahl2;
            }
            else if (Operator == "-")
            {
                ergebnis = ergebnis + zahl2;
            }
            return ergebnis;
        } //vorzeichen zuordnen
        public static char[] Zahlen = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        public static char[] Operatoren = { '-', '+', '*', '/', '^', '(', ')' };

    }
}
