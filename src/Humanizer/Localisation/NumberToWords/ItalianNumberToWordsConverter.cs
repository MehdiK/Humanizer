﻿using System;
using System.Collections.Generic;

namespace Humanizer.Localisation.NumberToWords
{
    internal class ItalianNumberToWordsConverter : GenderedNumberToWordsConverter
    {
        public override string Convert(int number, GrammaticalGender gender)
        {
            if (number == 0) 
                return "zero";

            if (number < 0)
                return "meno " + Convert(Math.Abs(number), gender);
        
            ItalianNumberCruncher cruncher = new ItalianNumberCruncher(number);
            
            return cruncher.Convert();
        }

        public override string ConvertToOrdinal(int number, GrammaticalGender gender)
        {
            return String.Empty;
        }
        
        class ItalianNumberCruncher
        {
            public ItalianNumberCruncher(int number)
            {
                _number = number;
                _threeDigitParts = SplitEveryThreeDigits(_number);
                _nextSet = ThreeDigitSets.Units;
            }
            
            public string Convert()
            {
                string word = String.Empty;
                
                foreach(int part in _threeDigitParts)
                {
                    Func<int, string> partToString = GetNextChopConverter();
                    
                    word = partToString(part) + word;
                }
                
                return word;
            }
            
            protected readonly int _number;
            protected readonly List<int> _threeDigitParts;
            
            protected ThreeDigitSets _nextSet;
            
            /// <summary>
            /// Splits a number into a sequence of three-digits numbers, starting 
            /// from units, then thousands, millions, and so on.
            /// </summary>
            /// <param name="number">The number to split.</param>
            /// <returns>The sequence of three-digit numbers.</returns>
            protected static List<int> SplitEveryThreeDigits(int number)
            {
                List<int> parts = new List<int>();
                int rest = number;
                
                while (rest > 0)
                {
                    int threeDigit = rest % 1000;
                    
                    parts.Add(threeDigit);
                    
                    rest = (int)(rest / 1000);
                }
                
                return parts;
            }
            
            /// <summary>
            /// During number conversion to text, finds out the converter to use
            /// for the next three-digit pack.
            /// </summary>
            /// <returns>The next conversion function to use.</returns>
            public Func<int, string> GetNextChopConverter()
            {
                Func<int, string> converter;
                
                switch (_nextSet)
                {
                    case ThreeDigitSets.Units:
                        converter = UnitsConverter;
                        _nextSet = ThreeDigitSets.Thousands;
                        break;
                        
                    case ThreeDigitSets.Thousands:
                        converter = ThousandsConverter;
                        _nextSet = ThreeDigitSets.Millions;
                        break;
                        
                    case ThreeDigitSets.Millions:
                        converter = null;
                        break;
                        
                    case ThreeDigitSets.Billions:
                        converter = null;
                        break;
                        
                    case ThreeDigitSets.More:
                        converter = null;
                        break;
                        
                    default:
                        throw new ArgumentOutOfRangeException("Unknow ThreeDigitSet: " + _nextSet);
                }
                
                return converter;
            }
            
            /// <summary>
            /// Converts a three-digit number to text.
            /// </summary>
            /// <param name="number">The three-digit number to convert.</param>
            /// <returns>The same three-digit number expressed as text.</returns>
            protected static string UnitsConverter(int number)
            {
                int tensAndUnits = number % 100;
                int hundreds = (int)(number / 100);
                
                int units = tensAndUnits % 10;
                int tens = (int)(tensAndUnits / 10);
                
                string words = String.Empty;
                
                words += _hundredNumberToText[hundreds];
                
                words += _tensNumberToText[tens];
                
                if (tensAndUnits <= 9)
                {
                    words += _unitsNumberToText[tensAndUnits];
                }
                else if (tensAndUnits <= 19)
                {
                    words += _teensNumberToText[tensAndUnits - 10];
                }
                else
                {
                    if (units == 1 || units == 8)
                    {
                        words = words.Remove(words.Length - 1);
                    }
                    
                    words += _unitsNumberToText[units];
                }
                
                return words;
            }
            
            /// <summary>
            /// Converts a thousands three-digit number to text.
            /// </summary>
            /// <param name="number">The three-digit number, as thousands, to convert.</param>
            /// <returns>The same three-digit number of thousands expressed as text.</returns>
            protected static string ThousandsConverter(int number)
            {
                return UnitsConverter(number) + "mila";
            }
            
            /// <summary>
            /// Lookup table converting units number to text. Index 1 for 1, index 2 for 2, up to index 9.
            /// </summary>
            protected static string[] _unitsNumberToText = new string[]
            {
                String.Empty,
                "uno",
                "due",
                "tre",
                "quattro",
                "cinque",
                "sei",
                "sette",
                "otto",
                "nove"
            };
            
            /// <summary>
            /// Lookup table converting tens number to text. Index 2 for 20, index 3 for 30, up to index 9 for 90.
            /// </summary>
            protected static string[] _tensNumberToText = new string[]
            {
                String.Empty,
                String.Empty,
                "venti",
                "trenta",
                "quaranta",
                "cinquanta",
                "sessanta",
                "settanta",
                "ottanta",
                "novanta"
            };
            
            /// <summary>
            /// Lookup table converting teens number to text. Index 0 for 10, index 1 for 11, up to index 9 for 19.
            /// </summary>
            protected static string[] _teensNumberToText = new string[]
            {
                "dieci",
                "undici",
                "dodici",
                "tredici",
                "quattordici",
                "quindici",
                "sedici",
                "diciassette",
                "diciotto",
                "diciannove"
            };
            
            /// <summary>
            /// Lookup table converting hundreds number to text. Index 0 for no hundreds, index 1 for 100, up to index 9.
            /// </summary>
            protected static string[] _hundredNumberToText = new string[]
            {
                String.Empty,
                "cento",
                "duecento",
                "trecento",
                "quattrocento",
                "cinquecento",
                "seicento",
                "settecento",
                "ottocento",
                "novecento"
            };
            
            /// <summary>
            /// Enumerates sets of three-digits having distinct conversion to text.
            /// </summary>
            protected enum ThreeDigitSets
            {
                /// <summary>
                /// Lowest three-digits set, from 1 to 999.
                /// </summary>
                Units,
                
                /// <summary>
                /// Three-digits set counting the thousands, from 1'000 to 999'000.
                /// </summary>
                Thousands,
                
                /// <summary>
                /// Three-digits set counting millions, from 1'000'000 to 999'000'000.
                /// </summary>
                Millions,
                
                /// <summary>
                /// Three-digits set counting billions, from 1'000'000'000 to 999'000'000'000.
                /// </summary>
                Billions,
                
                /// <summary>
                /// Three-digits set beyond 999 billions, from 1'000'000'000'000 onward.
                /// </summary>
                More
            }
        }
    }
}