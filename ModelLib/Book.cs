using System;

namespace ModelLib
{
    public class Book
    {
        private string _title;
        private string _auther;
        private int _pageNo;
        private string _isbn13;

        public Book(string title, string auther, int pageNo, string isbn13)
        {
            Title = title;
            Auther = auther;
            PageNo = pageNo;
            Isbn13 = isbn13;
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                CheckTitle(value);
            }
        }

        public string Auther
        {
            get => _auther;
            set => _auther = value;
        }

        public int PageNo
        {
            get => _pageNo;
            set
            {
                _pageNo = value;
                CheckPageNo(value);
            }
        }

        public string Isbn13
        {
            get => _isbn13;
            set
            {
                _isbn13 = value;
                CheckIsbn13(value);
            }
        }

        private void CheckTitle(string value)
        {
            if (value.Length < 2)
            {
                throw new ArgumentException("You need at least 2 characters");
            }
        }
        private void CheckPageNo(int value)
        {
            if (value < 10 || value > 1000)
            {
                throw new ArgumentException("Pagenumber must be between 10 & 1000");
            }
        }
        private void CheckIsbn13(string value)
        {
            if (value.Length != 13)
            {
                throw new ArgumentException("Isbn must be 13 characters");
            }
        }
    }
}

