using System;
using System.Collections.Generic;
using System.Text;

namespace GleichungsRechner
{
    public class Berechne
    {
        private static List<string> _z = new List<string>();
        private static List<string> _o = new List<string>();
        private static char[] Zahlen = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        private static char[] Operatoren = { '-', '+', '*', '/', '^', '(', ')' };
        private static double _ergebnis = 0;

        private static void Parser(string input)
        {
            char[] inputCh;
            inputCh = input.ToCharArray();

            for (int i = 0; i < inputCh.Length; i++)
            {
                if (isOperator(inputCh[i]))
                {
                    _o.Add(inputCh[i].ToString());
                }
                else if (isZahl(inputCh[i]))
                {
                    if(i + 1 < inputCh.Length)
                    {
                        if (isZahl(inputCh[i]) && isZahl(inputCh[i + 1]))
                        {
                            _z.Add((Convert.ToDouble(inputCh[i].ToString()) * 10 + Convert.ToDouble(inputCh[i + 1].ToString())).ToString());
                            i += 1;
                        }
                        else
                        {
                            _z.Add(inputCh[i].ToString());
                        }
                    }
                    else
                    {
                        _z.Add(inputCh[i].ToString());
                    }
                }
            }
        }
        private static void VorzeichenBerechner()
        {
            if (_o.Count == _z.Count)
            {
                double erstezahl = Convert.ToInt32(_z[0]);
                if (_o[0] == "-")
                {
                    erstezahl = erstezahl - (erstezahl * 2);
                    _z[0] = erstezahl.ToString();
                    _o.RemoveAt(0);
                }
                else
                {
                    _o.RemoveAt(0);
                }
            }
        }
        private static void PlusZuMinusConverter()
        {
            for (int x = 0; _o.Count > x; x++)
            {
                double zDou = Convert.ToDouble(_z[x + 1]);
                if (_o[x] == "-")
                {
                    zDou = zDou - (zDou * 2);
                    _z[x + 1] = zDou.ToString();
                }
            }
        }
        private static double PunktVorStrich(List<string> o, List<string> z)
        {
            double ergebnis = 0;
            for(; o.Count > 0;)
            {
                if (o.Contains("^"))
                {
                    int stelle = o.IndexOf("^");
                    ergebnis = Berechner("^", z[stelle], z[stelle + 1]);
                    o.RemoveAt(stelle);
                    z.RemoveAt(stelle);
                    z[stelle] = ergebnis.ToString();
                    continue;
                }

                if (o.Contains("*") || (o.Contains("/")))
                {
                    string op = "/";
                    int stelleDiff = o.IndexOf("/");
                    int stelleMul = o.IndexOf("*");
                    var stelle = stelleDiff;
                    
                    if(stelleMul > -1 && (stelleMul < stelleDiff) || stelleDiff == -1)
                    {
                        op = "*";
                        stelle = stelleMul;
                    }
                    ergebnis = Berechner(op, z[stelle], z[stelle + 1]);
                    o.RemoveAt(stelle);
                    z.RemoveAt(stelle);
                    z[stelle] = ergebnis.ToString();
                    continue;
                }

                if (o.Contains("+"))
                {
                    int stelle = o.IndexOf("+");
                    ergebnis = Berechner("+", z[stelle], z[stelle + 1]);
                    o.RemoveAt(stelle);
                    z.RemoveAt(stelle);
                    z[stelle] = ergebnis.ToString();
                    continue;
                }

                if (o.Contains("-"))
                {
                    int stelle = o.IndexOf("-");
                    ergebnis = Berechner("-", z[stelle], z[stelle + 1]);
                    o.RemoveAt(stelle);
                    z.RemoveAt(stelle);
                    z[stelle] = ergebnis.ToString();
                    continue;
                } 
            }
            return ergebnis;
        }
        private static void Klamer()
        {
            if (_o.Contains("("))
            {
                var klammerAnfang = _o.IndexOf("(");
                var klammerEnde = _o.IndexOf(")");
                List<string> KlammerO = new List<string>();
                List<string> KlammerZ = new List<string>();

                for (int x = klammerAnfang; x < klammerEnde; x++)
                {
                    KlammerZ.Add(_z[x]);
                }
                for (int x = klammerAnfang; x + 1 < klammerEnde; x++)
                {
                    KlammerO.Add(_o[x + 1]);
                }

                _z[klammerAnfang] = PunktVorStrich(KlammerO, KlammerZ).ToString();

                for (int x = klammerAnfang + 1; x < klammerEnde; x++)
                {
                    _z.RemoveAt(x);
                }
                for (int x = klammerAnfang; x <= klammerEnde; x++)
                {
                    _o.RemoveAt(klammerAnfang);
                }
            }
            else
            {
                return;
            }           
        }
        private static void Clear() 
        {
            _z.Clear();
            _o.Clear();
            _ergebnis = 0;
        }

        public static double Term(string input)
        {
            Clear();
            Parser(input);
            VorzeichenBerechner();
            Klamer();
            PlusZuMinusConverter();
            _ergebnis = PunktVorStrich(_o, _z);
 
            return _ergebnis;
        }

        private static bool isOperator(char input)
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
        private static bool isZahl(char input)
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
        private static double Berechner(string Operator, string zahl, string zahl2)
        {
            var ergebnis = Convert.ToDouble(zahl);
            var ergebnis2 = ergebnis;
            var zahlDou = Convert.ToDouble(zahl2);

            if (Operator == "^")
            {
                for (int x = 1; zahlDou > x; x++)
                {
                    ergebnis = ergebnis * ergebnis2;
                }
            }
            else if (Operator == "*")
            {
                ergebnis = ergebnis * zahlDou;
            }
            else if (Operator == "/")
            {
                ergebnis = ergebnis / zahlDou;
            }
            else if (Operator == "+")
            {
                ergebnis = ergebnis + zahlDou;
            }
            else if (Operator == "-")
            {
                ergebnis = ergebnis + zahlDou;
            }
            return ergebnis;
        } //vorzeichen zuordnen       
    }
}
