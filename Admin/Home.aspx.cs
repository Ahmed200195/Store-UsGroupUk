using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Store.Admin
{
    public partial class Home : System.Web.UI.Page
    {
        ClsBasic clsBasic;
        DataTable dataTable;
        protected void Page_Load(object sender, EventArgs e)
        {
            clsBasic = new ClsBasic();
            dataTable = clsBasic.SelectData("COUNT(DISTINCT(Phone)) , 1 as filter", @"
                Client
                UNION
                SELECT SUM(Cnt) , 2 as filter FROM Orders
                UNION
                SELECT SUM(Total) , 3 as filter FROM Orders
                UNION
                SELECT COUNT(received) , 4 as filter FROM Client WHERE received = 1
                UNION
                SELECT COUNT(received) , 5 as filter FROM Client WHERE received = 0
                UNION
                SELECT COUNT(Id) , 6 as filter FROM Product
                UNION
                SELECT COUNT(Id) , 7 as filter FROM Dept
                UNION
                SELECT COUNT(Id) , 8 as filter FROM Product WHERE Cnt > 0
                ORDER BY filter
            ");
            cntClient.InnerText = dataTable.Rows[0][0].ToString();
            cntOrders.InnerText = dataTable.Rows[1][0].ToString();
            cntSales.InnerText = dataTable.Rows[2][0].ToString();
            CntRecive.InnerText = dataTable.Rows[3][0].ToString();
            CntUnRecive.InnerText = dataTable.Rows[4][0].ToString();

            CntProduct.InnerText = dataTable.Rows[5][0].ToString();
            CntDept.InnerText = dataTable.Rows[6][0].ToString();
            CntYesProduct.InnerText = dataTable.Rows[7][0].ToString();

        }
    }
}