﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDEmployeeMVC.Model
{
    public class Pagination
    {
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }

        public Pagination()
        {

        }
        public Pagination(int totalItems, int page, int pageSize = 3)
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems/ (decimal)pageSize);
            int currentPage = page;
            int startPage = currentPage - 4;
            int endPage = currentPage + 4;

            if (startPage <= 0)
            {
                endPage = currentPage + 4;
                startPage = 1;
            }

            if(endPage > totalPages)
            {
                endPage = totalPages;
                if  (endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;

        }
    }
}
