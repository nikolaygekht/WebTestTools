using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gehtsoft.Webview2.Uitest
{
    public static class WebBrowserDriverExtensions
    {
        /// <summary>
        /// Waits until the predicate returns true
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="predicate"></param>
        /// <param name="timeoutInSeconds"></param>
        public static void WaitFor(this WebBrowserDriver driver, Func<WebBrowserDriver, bool> predicate, double? timeoutInSeconds = null)
        {
            DateTime start = DateTime.Now;
            TimeSpan? waitTime = null;
            if (timeoutInSeconds != null)
                waitTime = TimeSpan.FromSeconds((double)timeoutInSeconds);

            while (!predicate(driver))
            {
                Thread.Sleep(1);
                if (waitTime != null && (DateTime.Now - start) > waitTime.Value)
                    throw new TimeoutException();
            }
        }

        /// <summary>
        /// Wait while the predicate returns true.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="predicate"></param>
        /// <param name="timeoutInSeconds"></param>
        public static void WaitUntil(this WebBrowserDriver driver, Func<WebBrowserDriver, bool> predicate, double? timeoutInSeconds = null)
            => WaitFor(driver, (d) => !predicate(d), timeoutInSeconds);
    }
}
