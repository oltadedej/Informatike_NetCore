﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversityDb_Infor.Utils
{
  public static class Extensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> items)
        {
            return items == null || !items.Any();
        }
    }
}
