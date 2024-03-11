using System;
using System.Collections.Generic;
using System.Linq;

namespace ChequeMicroservice.Application.Common.Extensions
{
    public static class Extensions
    {
        public static ICollection<T> SkipAndTake<T>(this ICollection<T> list, out int count, int? skip = 0, int? take = 0)
        {
            ICollection<T> items = default;
            count = 0;
                if (list != null)
                {
                    count = list.Count;
                    if (skip == 0 && take == 0)
                    {
                        items = list;
                    }
                    else if (skip > 0 && take == 0)
                    {
                        items = list.Skip(skip.Value).ToList();
                    }
                    else if (skip == 0 && take > 0)
                    {
                        items = list.Take(take.Value).ToList();
                    }
                    else
                    {
                        items = list.Skip(skip.Value).Take(take.Value).ToList();
                    }
                }
            return items;
        }

        public static IQueryable<T> SkipAndTake<T>(this IQueryable<T> list, out int count, int? skip = 0, int? take = 0)
        {
            IQueryable<T> items = default;
            count = 0;
          
                if (list != null)
                {
                    count = list.Count();
                    if (skip == 0 && take == 0)
                    {
                        items = list;
                    }
                    else if (skip > 0 && take == 0)
                    {
                        items = list.Skip(skip.Value);
                    }
                    else if (skip == 0 && take > 0)
                    {
                        items = list.Take(take.Value);
                    }
                    else
                    {
                        items = list.Skip(skip.Value).Take(take.Value);
                    }
                }
            return items;
        }

        public static List<T> SkipAndTake<T>(this List<T> list, out int count, int? skip = 0, int? take = 0)
        {
            List<T> items = default(List<T>);
            count = 0;
                if (list != null)
                {
                    count = list.Count;
                    if (skip == 0 && take == 0)
                    {
                        items = list;
                    }
                    else if (skip > 0 && take == 0)
                    {
                        items = list.Skip(skip.Value).ToList();
                    }
                    else if (skip == 0 && take > 0)
                    {
                        items = list.Take(take.Value).ToList();
                    }
                    else
                    {
                        items = list.Skip(skip.Value).Take(take.Value).ToList();
                    }
                }
           
            return items;
        }

        public static List<T> SkipAndTake<T>(this List<T> list, int? skip = 0, int? take = 0)
        {
            int count = 0;
            return list.SkipAndTake(out count, skip, take);
        }

        public static ICollection<T> SkipAndTake<T>(this ICollection<T> list, int? skip = 0, int? take = 0)
        {
            int count = 0;
            return list.SkipAndTake(out count, skip, take);
        }

        public static IQueryable<T> SkipAndTake<T>(this IQueryable<T> list, int? skip = 0, int? take = 0)
        {
            int count = 0;
            return list.SkipAndTake(out count, skip, take);
        }

        public static PaginatedList<T> Paginate<T>(this List<T> list, int? skip = 0, int? take = 0)
        {
            PaginatedList<T> result = new PaginatedList<T>();
                List<T> items = new List<T>();
                int count = 0;
                if (list != null)
                {
                    if (skip == 0 && take == 0)
                    {
                        items = list;
                    }
                    else if (skip > 0 && take == 0)
                    {
                        items = list.Skip(skip.Value).ToList();
                    }
                    else if (skip == 0 && take > 0)
                    {
                        items = list.Take(take.Value).ToList();
                    }
                    else
                    {
                        items = list.Skip(skip.Value).Take(take.Value).ToList();
                    }
                count = list.Count;
                }
                result = new PaginatedList<T>
                {
                    PageItems = items,
                    TotalCount = count
                };
            return result;
        }

        public static PaginatedList<T> Paginate<T>(this ICollection<T> list, int? skip = 0, int? take = 0)
        {
            PaginatedList<T> result = new PaginatedList<T>();
            int count = 0;
            ICollection<T> items = default;
            if (list != null)
            {
                if (skip == 0 && take == 0)
                {
                    items = list;
                }
                else if (skip > 0 && take == 0)
                {
                    items = list.Skip(skip.Value).ToList();
                }
                else if (skip == 0 && take > 0)
                {
                    items = list.Take(take.Value).ToList();
                }
                else
                {
                    items = list.Skip(skip.Value).Take(take.Value).ToList();
                }
                count = list.Count;
            }
            result = new PaginatedList<T>
            {
                PageItems = items.ToList(),
                TotalCount = count
            };
            return result;
        }

        public static PaginatedList<T> Paginate<T>(this IQueryable<T> list, int? skip = 0, int? take = 0)
        {
            PaginatedList<T> result = new PaginatedList<T>();
            int count = 0;
            IQueryable<T> items = default;
            if (list != null)
            {
                if (skip == 0 && take == 0)
                {
                    items = list;
                }
                else if (skip > 0 && take == 0)
                {
                    items = list.Skip(skip.Value);
                }
                else if (skip == 0 && take > 0)
                {
                    items = list.Take(take.Value);
                }
                else
                {
                    items = list.Skip(skip.Value).Take(take.Value);
                }
                count = list.Count();
            }
                result = new PaginatedList<T>
                {
                    PageItems = items.ToList(),
                    TotalCount = count
                };
            return result;
        }


        public class PaginatedList<T>
        {
            public ICollection<T> PageItems { get; set; }
            public int PageCount { get { return PageItems == null ? 0 : this.PageItems.Count; } }
            public int TotalCount { get; set; }
        }
    }
}
