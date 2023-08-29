﻿using DATA.Repository.Implementation.Debugging.Interfaces;
using System.Diagnostics;

namespace DATA.Repository.Implementation.Debugging.Implementations
{
    public class DefaultDebugStrategy<T> : IDebugStrategy<T> where T : BaseEntity
        {
            public void BeforeHook(IQueryable<T> query, Filter<T> filter, DebugContext context, string sectionName)
            {
                Debug.WriteLine($"Before {sectionName}:");
                context.Stopwatch.Start();
                Debug.WriteLine($"Query: {query}");
              

                var dataSnapshot = query.ToList();
                Debug.WriteLine($"Data Count: {dataSnapshot.Count}");
             }

            public void AfterHook(IQueryable<T> query, Filter<T> filter, DebugContext context, string sectionName)
            {
                Debug.WriteLine($"After {sectionName}:");
                Debug.WriteLine($"Query: {query}");
                var dataSnapshot = query.ToList();
                Debug.WriteLine($"Data Count: {dataSnapshot.Count}");

                context.Stopwatch.Stop();
                Debug.WriteLine($"{sectionName} took {context.Stopwatch.ElapsedMilliseconds} ms");
                context.Stopwatch.Reset();
            }
        }

    }
