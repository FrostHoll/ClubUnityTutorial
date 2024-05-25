using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Random = UnityEngine.Random;

namespace TutCSharp.RandomSystem
{
    public class RandomTable<T> where T : notnull
    {
        protected Dictionary<T, int> _table;

        protected Dictionary<T, int> _oldTable;

        /// <summary>
        /// If set to <c>false</c>, automatically saves all changes have been made at every new change.
        /// Otherwise, call <see cref="SaveChanges"/> method to save changes.
        /// <para>Default value is <c>false</c>.</para>
        /// </summary>
        public bool ManualSavingChanges { get; set; }

        /// <summary>
        /// Adds a new element <paramref name="newElement"/> to the table
        /// with capacity <paramref name="capacity"/>.
        /// </summary>
        /// <param name="newElement">An element to add in a table.</param>
        /// <param name="capacity">New element's capacity</param>
        /// <exception cref="ArgumentOutOfRangeException">Throws if <paramref name="capacity"/> is below 1.</exception>
        public void Add(T newElement, int capacity)
        {
            if (capacity < 1) throw new ArgumentOutOfRangeException(nameof(capacity));
            _table.Add(newElement, capacity);
            _oldTable.Add(newElement, capacity);
        }

        /// <summary>
        /// Returns random element from a table.
        /// </summary>
        /// <returns>Random element from table.</returns>
        /// <exception cref="KeyNotFoundException">Throws if a table is empty.</exception>
        public virtual T Next()
        {
            int randVal = Random.Range(1, GetCurrentCapacity() + 1);
            foreach (var el in _table)
            {
                if (randVal > el.Value)
                {
                    randVal -= el.Value;
                }
                else
                {
                    return el.Key;
                }
            }
            throw new KeyNotFoundException();
        }

        /// <summary>
        /// Get elements and their probability in string.
        /// </summary>
        /// <returns>Elements and their probability.</returns>
        public string GetTableInfo()
        {
            StringBuilder sb = new StringBuilder();
            int capacity = GetCurrentCapacity();
            foreach (var el in _table)
            {
                sb.Append('(');
                sb.Append(el.Key.ToString());
                sb.Append("): ");
                sb.Append((float)el.Value / capacity * 100);
                sb.Append("%\n");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Change elements' capacity by a factor <paramref name="factorValue"/>
        /// which match to predicate <paramref name="matchPredicate"/>.
        /// </summary>
        /// <param name="matchPredicate">Predicate specifying which elements to change their capacity.</param>
        /// <param name="factorValue">Factor value to change elements probability.</param>
        /// <exception cref="ArgumentOutOfRangeException">Throws if <paramref name="factorValue"/> is less than zero.</exception>
        /// <exception cref="ArgumentException">Throws if there are no elements to match <paramref name="matchPredicate"/>.</exception>
        public void ChangeElementsCapacity(Predicate<T> matchPredicate, float factorValue = 1.0f)
        {
            if (factorValue < 0.0f) throw new ArgumentOutOfRangeException(nameof(factorValue));
            if (!ManualSavingChanges) SaveChanges();
            foreach (var el in _table)
                if (matchPredicate(el.Key))
                    _table[el.Key] = Convert.ToInt32(el.Value * factorValue);
            if (GetCurrentCapacity() <= 0) throw new ArgumentException(nameof(matchPredicate));
        }

        /// <summary>
        /// Loads all elements' capacities which were previously saved.
        /// </summary>
        public void RestoreChanges()
        {
            if (_oldTable.Count == 0) return;
            foreach (var item in _table)
            {
                if (_oldTable.ContainsKey(item.Key))
                {
                    _table[item.Key] = _oldTable[item.Key];
                }
            }
        }

        /// <summary>
        /// Creates new instance of <see cref="RandomTable{T}"/> with specified <see cref="Random"/> instance.
        /// </summary>
        /// <param name="random">Specified <see cref="Random"/> instance.</param>
        public RandomTable()
        {
            _table = new();
            _oldTable = new();
            ManualSavingChanges = false;
        }

        /// <summary>
        /// Saves elements' capacities. 
        /// <para>This method is called automatically in <see cref="ChangeElementsCapacity(Predicate{T}, float)"/>
        /// if <see cref="ManualSavingChanges"/> is set to <c>false</c>.</para>
        /// </summary>
        public void SaveChanges()
        {
            if (_oldTable.Count == 0)
            {
                foreach (var item in _table)
                {
                    _oldTable.Add(item.Key, item.Value);
                }
            }
            else
            {
                foreach (var item in _table)
                {
                    _oldTable[item.Key] = item.Value;
                }
            }
        }

        protected int GetCurrentCapacity()
        {
            int capacity = 0;
            foreach (var el in _table)
            {
                capacity += el.Value;
            }
            return capacity;
        }
    }
}

