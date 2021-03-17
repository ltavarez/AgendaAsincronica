using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class LongTask
    {
        public static async Task<int> StartLongTask()
        {
            int i = 0;

            await Task.Run(() =>
            {
                i = LongTaskMethod();
            });           

            return i;
        }

        private static int LongTaskMethod()
        {
            var range = Enumerable.Range(1, 50);

            foreach(int item in range)
            {
                Thread.Sleep(500);
                var h = item;
            }

            return 1;

        }

    }
}
