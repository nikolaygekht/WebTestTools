using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Gehtsoft.Webtest.Spellchecker
{
    /// <summary>
    /// The factory for spellchecker
    /// </summary>
    public sealed class SpellCheckerFactory
    {
        private readonly object mMutex = new object();
        private readonly Dictionary<string, Spellchecker> mDictionary = new Dictionary<string, Spellchecker>();

        private static SpellCheckerFactory gInstance;

        /// <summary>
        /// Returns the instance of the factory.
        /// </summary>
        public static SpellCheckerFactory Instance => gInstance ??= new SpellCheckerFactory();

        /// <summary>
        /// Returns a spell checker by the language name.
        /// </summary>
        /// <param name="language"></param>
        /// <returns></returns>
        public Spellchecker this[string language]
        {
            get
            {
                lock (mMutex)
                {
                    if (!mDictionary.TryGetValue(language, out Spellchecker sp))
                    {
                        sp = new Spellchecker(DictionaryLocationCallback, language);
                        mDictionary[language] = sp;
                    }
                    return sp;
                }
            }
        }

        private SpellCheckerFactory()
        {
        }

        /// <summary>
        /// Initializes the spellchecker using the language and the list of the exemptions.
        /// </summary>
        /// <param name="language"></param>
        /// <param name="exemptions"></param>
        public void Initialize(string language, IEnumerable<string> exemptions = null)
        {
            lock (mMutex)
            {
                if (mDictionary.TryGetValue(language, out Spellchecker sp) &&
                    sp.ExceptionListMatch(exemptions))
                    return;

                sp = new Spellchecker(DictionaryLocationCallback, language, exemptions);
                mDictionary[language] = sp;
            }
        }

        /// <summary>
        /// Resets all initialized dictionaries.
        /// </summary>
        public void Reset()
        {
            mDictionary.Clear();
        }

        /// <summary>
        /// <para>The optional callback that returns the URL by the language name</para>
        /// <para>You can override this function to set your source to download the dictionaries.
        /// If the source is not set, the dictionaries will be downloaded from https://docs.gehtsoftusa.com</para>
        /// </summary>
        public Func<string, string> DictionaryLocationCallback { get; set; } = (language) => $"https://docs.gehtsoftusa.com/bin/dic/{language}";
    }
}
