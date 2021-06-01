using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Gehtsoft.Webtest.Spellchecker
{
    public class SpellCheckerFactory
    {
        private readonly object mMutex = new object();
        private readonly Dictionary<string, Spellchecker> mDictionary = new Dictionary<string, Spellchecker>();

        private static SpellCheckerFactory gInstance;

        public static SpellCheckerFactory Instance => gInstance ??= new SpellCheckerFactory();

        public Spellchecker this[string language]
        {
            get
            {
                lock (mMutex)
                {
                    if (!mDictionary.TryGetValue(language, out Spellchecker sp))
                    {
                        sp = new Spellchecker(language);
                        mDictionary[language] = sp;
                    }
                    return sp;
                }
            }
        }

        public void Initialize(string language, IEnumerable<string> exemptions = null)
        {
            lock (mMutex)
            {
                if (mDictionary.TryGetValue(language, out Spellchecker sp) &&
                    sp.ExceptionListMatch(exemptions))
                    return;

                sp = new Spellchecker(language, exemptions);
                mDictionary[language] = sp;
            }
        }

        public void Reset()
        {
            mDictionary.Clear();
        }
    }
}
