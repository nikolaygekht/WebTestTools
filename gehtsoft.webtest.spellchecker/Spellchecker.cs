using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WeCantSpell.Hunspell;

namespace Gehtsoft.Webtest.Spellchecker
{
    public class Spellchecker
    {
        private readonly WordList mDictionary;
        private readonly HashSet<string> mExemptions = null;

        public Spellchecker(string language = "en_US", IEnumerable<string> exemptions = null)
        {
            string f = EnsureDictionary(language);
            mDictionary = WordList.CreateFromFiles(f);

            mExemptions = new HashSet<string>();
            if (exemptions != null)
                foreach (string s in exemptions)
                    mExemptions.Add(s.ToLower());
        }

        public bool SpellOne(string word)
        {
            if (mExemptions.Contains(word.ToLower()))
                return true;

            return mDictionary.Check(word);
        }

        private readonly static Regex gWordParser = new Regex(@"[\w']+");

        public List<string> SpellMany(string text)
        {
            Match m;
            List<string> o = null;
            while (true)
            {
                m = gWordParser.Match(text);

                if (!m.Success)
                    break;

                if (!SpellOne(m.Groups[0].Value))
                    (o ??= new List<string>()).Add(m.Groups[0].Value);

                text = text[(m.Groups[0].Index + m.Groups[0].Length)..];
            }
            return o;
        }

        internal bool ExceptionListMatch(IEnumerable<string> exemptionList)
        {
            if (exemptionList == null && mExemptions.Count == 0)
                return true;

            if (exemptionList == null)
                return false;

            var arr = exemptionList.ToArray();
            if (arr.Length != mExemptions.Count)
                return false;

            for (int i = 0; i < arr.Length; i++)
                if (!mExemptions.Contains(arr[i]))
                    return false;

            return true;
        }

        private void EnsureFile(string folder, string file, string source)
        {
            string ff = Path.Combine(folder, file);
            if (!File.Exists(ff))
            {
                using WebClient wc = new WebClient();
                wc.DownloadFile($"{source}/{file}", ff);
            }
        }

        private string EnsureDictionary(string language = "en_US")
        {
            FileInfo fi = new FileInfo(typeof(Spellchecker).Assembly.Location);
            string folder = fi.Directory.FullName;
            EnsureFile(folder, $"{language}.dic", "http://docs.gehtsoftusa.com/bin/dic");
            EnsureFile(folder, $"{language}.aff", "http://docs.gehtsoftusa.com/bin/dic");
            return Path.Combine(folder, $"{language}.dic");
        }
    }
}
