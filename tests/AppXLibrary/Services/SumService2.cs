
namespace AppXLibrary.Services
{
    public class SumService2
    {
        #region Public Constructors

        public SumService2()
        {
            m_sum = 100;
        }

        #endregion

        #region Public Methods

        public void Add(int number)
        {
            m_sum += number;
        }

        public int GetSum()
        {
            return(m_sum);
        }

        #endregion

        #region Private Fields

        private int m_sum;

        #endregion
    }
}
