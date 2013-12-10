using System;
using System.Collections;
using System.Collections.Generic;
namespace Stanowisko.Symulator
{
    class EstimatingFunctionFactory
    {
        private static SimpleEstimatingFunction _simple = null;

        public static SimpleEstimatingFunction Simple
        {
            get
            {
                if (_simple == null)
                {
                    _simple = new SimpleEstimatingFunction();
                }
                return _simple;
            }
        }

        public static IEnumerable EstimatingFunctionsList
        {
            get
            {
                return new IEstimatingFunctionsEnumerable(new IEstimatingFunction[] {Simple});
            }
        }

        public class IEstimatingFunctionsEnumerable : IEnumerable
        {
            private IEstimatingFunction[] _estimatingFunctionsList;

            public IEstimatingFunctionsEnumerable(IEstimatingFunction[] list)
            {
                _estimatingFunctionsList = list;
            }

            IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return new IEstimatingFunctionsEnumerator(_estimatingFunctionsList);
            }
        }

        public class IEstimatingFunctionsEnumerator : IEnumerator<IEstimatingFunction>
        {
            public IEstimatingFunction[] _estimatingFunctionsList;

            int position = -1;

            public IEstimatingFunctionsEnumerator(IEstimatingFunction[] list)
            {
                _estimatingFunctionsList = list;
            }

            public bool MoveNext()
            {
                position++;
                return (position < _estimatingFunctionsList.Length);
            }

            public void Reset()
            {
                position = -1;
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public IEstimatingFunction Current
            {
                get
                {
                    try
                    {
                        return _estimatingFunctionsList[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            void System.IDisposable.Dispose() { }
        }
    }
}
