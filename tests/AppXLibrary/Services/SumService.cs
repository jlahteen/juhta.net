
namespace AppXLibrary.Services
{
    public class SumService
    {
        #region Public Constructors

        public SumService(int baseSum)
        {
            m_sum = baseSum;
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
