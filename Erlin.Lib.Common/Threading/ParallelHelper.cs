using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Erlin.Lib.Common.Threading
{
    /// <summary>
    /// Helper for parallel tasks
    /// </summary>
    public static class ParallelHelper
    {
        /// <summary>Executes a <see langword="for" /> (<see langword="For" /> in Visual Basic) loop in which iterations may run in parallel.</summary>
        /// <param name="fromInclusive">The start index, inclusive.</param>
        /// <param name="toExclusive">The end index, exclusive.</param>
        /// <param name="body">The delegate that is invoked once per iteration.</param>
        /// <param name="oneThread">Whether this loop should use only one-thread for</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.</exception>
        /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
        public static void For(int fromInclusive, int toExclusive, Action<int> body, bool oneThread = false)
        {
            if (oneThread)
            {
                for (int i = fromInclusive; i < toExclusive; i++)
                {
                    body(i);
                }
            }
            else
            {
                Parallel.For(fromInclusive, toExclusive, body);
            }
        }

        /// <summary>Executes a <see langword="for" /> loop in which iterations may run in parallel for 2D array.</summary>
        /// <param name="width">Width of 2D</param>
        /// <param name="height">Height of 2D</param>
        /// <param name="body">The delegate that is invoked once per iteration.</param>
        /// <param name="oneThread">Whether this loop should use only one-thread for</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="body" /> argument is <see langword="null" />.</exception>
        /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
        public static void For2D(int width, int height, Action<int, int> body, bool oneThread = false)
        {
            int count = width * height;
            if (oneThread)
            {
                for (int i = 0; i < count; i++)
                {
                    int x = i % width;
                    int y = i / width;
                    body(x, y);
                }
            }
            else
            {
                Parallel.For(0, count, i =>
                                       {
                                           int x = i % width;
                                           int y = i / width;
                                           body(x, y);
                                       });
            }
        }

        /// <summary>Executes a <see langword="foreach" /> (<see langword="For Each" /> in Visual Basic) operation on an <see cref="T:System.Collections.IEnumerable" /> in which iterations may run in parallel.</summary>
        /// <param name="source">An enumerable data source.</param>
        /// <param name="body">The delegate that is invoked once per iteration.</param>
        /// <param name="oneThread">Whether this loop should use only one-thread foreach</param>
        /// <typeparam name="TSource">The type of the data in the source.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="source" /> argument is <see langword="null" />.
        /// -or-
        /// The <paramref name="body" /> argument is <see langword="null" />.</exception>
        /// <exception cref="T:System.AggregateException">The exception that contains all the individual exceptions thrown on all threads.</exception>
        public static void ForEach<TSource>(IEnumerable<TSource> source, Action<TSource> body, bool oneThread = false)
        {
            if (oneThread)
            {
                foreach (TSource fItem in source)
                {
                    body(fItem);
                }
            }
            else
            {
                Parallel.ForEach(source, body);
            }
        }

        /// <summary>Queues the specified work to run on the thread pool and returns a <see cref="T:System.Threading.Tasks.Task" /> object that represents that work.</summary>
        /// <param name="action">The work to execute asynchronously</param>
        /// <returns>A task that represents the work queued to execute in the ThreadPool.</returns>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="action" /> parameter was <see langword="null" />.</exception>
        public static Task Run(Action action)
        {
            string stackTrace = EnvironmentHelper.GetStackTrace();
            return Task.Run(() =>
                            {
                                try
                                {
                                    action();
                                }
                                catch (Exception ex)
                                {
                                    ex.Data.Add(ExtensionMethods.STACKTRACE_TASK, stackTrace);
                                    Log.Error(ex);
                                }
                            });
        }
    }
}