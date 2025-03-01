using Result;

namespace Store.Utils
{
    public static class PaginationHelper
    {
        public static Result<(int From, int To)> GetFromTo(int pageNumber, int pageSize, int totalCount)
        {
            if (pageNumber < 1)
            {
                return new Error("Page number can not be less than 1");
            }

            var pagesCount = (int)Math.Ceiling(totalCount / (double)pageSize);

            if (pageNumber > pagesCount)
            {
                return new Error("Page number is greater than total pages count");
            }

            var from = (pageNumber - 1) * pageSize;
            var to = from + pageSize > totalCount ? totalCount : from + pageSize;

            return (from, to);
        }
    }
}
