
namespace AppXLibrary.Services
{
    public class AggregateSumService
    {
        #region Public Constructors

        public AggregateSumService(SumService sumService1, SumService sumService2, SumService sumService3)
        {
            m_sumService1 = sumService1;

            m_sumService2 = sumService2;

            m_sumService3 = sumService3;
        }

        #endregion

        #region Public Methods

        public void Add(int number)
        {
            m_sumService1.Add(number);

            m_sumService2.Add(number);

            m_sumService3.Add(number);
        }

        public int GetSum()
        {
            return(m_sumService1.GetSum() + m_sumService2.GetSum() + m_sumService3.GetSum());
        }

        #endregion

        #region Private Fields

        private SumService m_sumService1;

        private SumService m_sumService2;

        private SumService m_sumService3;

        #endregion
    }
}
