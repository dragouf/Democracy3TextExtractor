﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Democracy3TextExtractor
{
    public static class StringExtensions
    {
        private readonly static Regex nonSpacingMarkRegex =
        new Regex(@"\p{Mn}", RegexOptions.Compiled);

        public static string RemoveDiacritics(this string text)
        {
            if (text == null)
                return string.Empty;

            var normalizedText =
                text.Normalize(NormalizationForm.FormD);

            return nonSpacingMarkRegex.Replace(normalizedText, string.Empty);
        }

        public static string DeleteAccentAndSpecialsChar(this string OriginalText)
        {
            string strTemp = OriginalText;
            // Regex creation
            Regex regA = new Regex("[ã|à|â|ä|á|å]");
            Regex regAA = new Regex("[Ã|À|Â|Ä|Á|Å]");
            Regex regE = new Regex("[é|è|ê|ë]");
            Regex regEE = new Regex("[É|È|Ê|Ë]");
            Regex regI = new Regex("[í|ì|î|ï]");            
            Regex regII = new Regex("[Í|Ì|Î|Ï]");
            Regex regL = new Regex("[ł]");
            Regex regLL = new Regex("[Ł]");
            Regex regO = new Regex("[õ|ò|ó|ô|ö]");
            Regex regOO = new Regex("[Õ|Ó|Ò|Ô|Ö]");
            Regex regU = new Regex("[ù|ú|û|ü|µ]");
            Regex regUU = new Regex("[Ü|Ú|Ù|Û]");
            Regex regY = new Regex("[ý|ÿ]");
            Regex regYY = new Regex("[Ý]");
            Regex regAE = new Regex("[æ]");
            Regex regAEAE = new Regex("[Æ]");
            Regex regOE = new Regex("[œ]");
            Regex regOEOE = new Regex("[Œ]");
            Regex regC = new Regex("[ç]");
            Regex regCC = new Regex("[Ç]");
            Regex regDD = new Regex("[Ð]");
            Regex regN = new Regex("[ñ]");
            Regex regNN = new Regex("[Ñ]");
            Regex regS = new Regex("[š]");
            Regex regSS = new Regex("[Š]");
            Regex regQ = new Regex("[’]");
            strTemp = regA.Replace(strTemp, "a");
            strTemp = regAA.Replace(strTemp, "A");
            strTemp = regE.Replace(strTemp, "e");
            strTemp = regEE.Replace(strTemp, "E");
            strTemp = regI.Replace(strTemp, "i");
            strTemp = regII.Replace(strTemp, "I");
            strTemp = regL.Replace(strTemp, "l");
            strTemp = regLL.Replace(strTemp, "l");
            strTemp = regO.Replace(strTemp, "o");
            strTemp = regOO.Replace(strTemp, "O");
            strTemp = regU.Replace(strTemp, "u");
            strTemp = regUU.Replace(strTemp, "U");
            strTemp = regY.Replace(strTemp, "y");
            strTemp = regYY.Replace(strTemp, "Y");
            strTemp = regAE.Replace(strTemp, "ae");
            strTemp = regAEAE.Replace(strTemp, "AE");
            strTemp = regOE.Replace(strTemp, "oe");
            strTemp = regOEOE.Replace(strTemp, "OE");
            strTemp = regC.Replace(strTemp, "c");
            strTemp = regCC.Replace(strTemp, "C");
            strTemp = regDD.Replace(strTemp, "D");
            strTemp = regN.Replace(strTemp, "n");
            strTemp = regNN.Replace(strTemp, "N");
            strTemp = regS.Replace(strTemp, "s");
            strTemp = regSS.Replace(strTemp, "S");
            strTemp = regQ.Replace(strTemp, "'");

            return strTemp;
        }

        //public static string ToUtf8(this string OriginalText)
        //{

        //}
    }
}
