﻿namespace Humanizer
{
    internal class AzerbaijaniOrdinalizer : DefaultOrdinalizer
    {
        public override string Convert(int number, string numberString)
        {
            return numberString + ".";
        }
    }
}
