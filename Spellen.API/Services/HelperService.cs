using System;
using System.Collections.Generic;
using Spellen.API.Models;

namespace Spellen.API.Services
{
    public class HelperService
    {
        public List<Item> ReturnUniqueItems(List<List<Item>> itemLists)
        {
            List<Item> UniqueItems = new List<Item>();

            foreach (List<Item> list in itemLists)
            {
                foreach (Item listItem in list)
                {
                    if (!UniqueItems.Contains(listItem))
                        UniqueItems.Add(listItem);
                }
            }

            return UniqueItems;
        }
    }
}
