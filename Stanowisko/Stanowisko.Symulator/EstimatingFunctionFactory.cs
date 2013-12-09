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

        public static IEnumerable<IEstimatingFunction> EstimatingFunctionsList
        {
            get
            {
                return null;
            }
        }
    }
}
