using Store.Admin;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Store.Client
{
    public partial class ShowProduct : System.Web.UI.Page
    {
        ClsBasic clsBasic;
        DataTable dtDept, dtSize, dtProduct;
        string active = string.Empty, url, OrginalUrl;
        string whereDept, whereSize, whereColor, whereSortBy, whereSaerch;


        protected void Page_Load(object sender, EventArgs e)
        {
                OrginalUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            if (!IsPostBack)
            {
                TextBox txtSearch = Master.FindControl("txtSearch") as TextBox;
                txtSearch.Text = Request.QueryString["search"] == "0" ? string.Empty : Request.QueryString["search"];
                if (!IsPostBack)
                {
                    ddlColors.DataBind();
                    ddlColors.Items.Insert(0, new ListItem() { Value = "0", Text = "default" });
                    ddlColors.SelectedValue = Request.QueryString["color"];
                    ddlSortBy.SelectedIndex = int.Parse(Request.QueryString["sortBy"]);
                }
                switch (ddlSortBy.SelectedIndex)
                {
                    case 0:
                        whereSortBy = " ORDER BY Id DESC";
                        break;
                    case 1:
                        whereSortBy = " ORDER BY NameEn";
                        break;
                    case 2:
                        whereSortBy = "  ORDER BY Price";
                        break;
                    case 3:
                        whereSortBy = "  ORDER BY Price DESC";
                        break;
                }

                clsBasic = new ClsBasic();
                dtDept = clsBasic.SelectData("*", "Dept WHERE Id IN (SELECT DId FROM Product)");
                dtSize = clsBasic.SelectData("*", "Size");

                whereDept = Request.QueryString["dept"] == "0" ? "" : " AND DId = " + Request.QueryString["dept"];

                whereSize = Request.QueryString["size"] == "0" ? "" : " AND SId = " + Request.QueryString["size"];

                whereColor = Request.QueryString["color"] == "0" ? "" : " AND CId = " + Request.QueryString["color"];

                whereSaerch = Request.QueryString["search"] == "0" ? "" : " AND (NameAr + NameEn + infoAr + infoEn) LIKE '%" + Request.QueryString["search"] + "%'";


                dtProduct = clsBasic.SelectData("*", "Product WHERE 1 = 1 " + whereDept + whereSize + whereColor + whereSaerch + whereSortBy);

                active = Request.QueryString["dept"] == "0" ? "text-[#b22234]" : "";
                url = OrginalUrl.Replace("dept=" + Request.QueryString["dept"], "dept=0");
                ltCategories.Text = string.Empty;
                ltCategories.Text += $@"
                    <li class=""px-1 py-2 cursor-pointer font-medium hover:text-[#b22234] capitalize {active}""><a href=""{url}"">All</a></li>
                ";
                foreach (DataRow row in dtDept.Rows)
                {
                    active = Request.QueryString["dept"] == row["Id"].ToString() ? "text-[#b22234]" : "";
                    url = OrginalUrl.Replace("dept=" + Request.QueryString["dept"], "dept=" + row["Id"]);
                    ltCategories.Text += $@"
                    <li class=""px-1 py-2 cursor-pointer font-medium hover:text-[#b22234] capitalize {active}""><a href=""{url}"">{row["NameEn"]}</a></li>
                ";
                }

                active = Request.QueryString["size"] == "0" ? "activeSize" : "";
                url = OrginalUrl.Replace("size=" + Request.QueryString["size"], "size=0");
                ltSizes.Text = string.Empty;
                ltSizes.Text = $@"
                    <a href=""{url}"" class=""selectedItem  notActive px-2 py-2 rounded font-medium hover:text-[#b22234] capitalize {active}"">All</a>
                ";
                foreach (DataRow row in dtSize.Rows)
                {
                    active = Request.QueryString["size"] == row["Id"].ToString() ? "activeSize" : "";
                    url = OrginalUrl.Replace("size=" + Request.QueryString["size"], "size=" + row["Id"]);
                    ltSizes.Text += $@"
                    <a href=""{url}"" class=""selectedItem  notActive px-2 py-2 rounded font-medium hover:text-[#b22234] capitalize {active}"">{row["Name"]}</a>
                ";
                }
                ltProdcut.Text = string.Empty;
                cntProduct.InnerText = dtProduct.Rows.Count + " ITEMS";
                string disc = string.Empty, finalPrice = string.Empty, discPrice = string.Empty;
                foreach (DataRow row in dtProduct.Rows)
                {
                    finalPrice = row["Price"].ToString();
                    if (int.Parse(row["Discount"].ToString()) > 0)
                    {
                        disc = $@"<span
                            class=""absolute top-0 left-0 m-2 rounded-full bg-[#b22234] px-2 text-center text-sm font-medium text-white"">{100 - ((int.Parse(row["Discount"].ToString()) * 100) / int.Parse(row["Price"].ToString()))}% OFF</span>";

                        discPrice = $@"<span class=""salePrice text-sm text-[#504f85] line-through uppercase"">${row["Price"]}</span>";
                        finalPrice = row["Discount"].ToString();
                    }
                    ltProdcut.Text += $@"
                                        <div
                                            class=""productElement relative  flex w-full  flex-col overflow-hidden rounded-lg border border-gray-100 bg-white shadow-lg transition-all duration-300 hover:shadow-2xl hover:-translate-y-1 "">
                                            <a class=""relative mx-3 mt-3 flex h-60 overflow-hidden rounded-xl"" href=""DetailsProduct.aspx?id={row["Id"]}"" target=""_blank"">
                                                <img class=""imgProduct object-fill w-full h-full"" src=""data:image;base64,{Convert.ToBase64String((byte[])row["Photo"])}""
                                                    alt=""product image"" />
                                            {disc}
                                            </a>
                                            <div class=""mt-4 px-5 pb-5"">
                                                <a href=""DetailsProduct.aspx?id={row["Id"]}"" target=""_blank"">
                                                    <div>
                                                        <h5 class=""text-xl tracking-tight text-[#504f85] product-name"">{row["NameEn"]}</h5>
                                                    </div>
                                                    <div class=""mt-2 mb-5 flex items-center justify-between"">
                                                        <p>
                                                            <span class=""text-xl font-normal text-[#504f85] product-price uppercase"">$<span>{finalPrice}</span>
                                                            </span>
                                                            {discPrice}
                                                        </p>

                                                    </div>
                                                </a>
                                                <!-- add to cart -->
                                                <div
                                                    class=""add-to-cart flex gap-2 items-center justify-center rounded-md bg-[#007a3d] cursor-pointer px-5 py-2.5 text-center text-sm font-medium text-white transition-all hover:bg-[#117a3d] focus:outline-none"">
                                                    <i class=""fa-solid fa-cart-shopping""></i>
                                                    Add to cart
                                                </div>
                                            </div>
                                        </div>
                    ";
                    disc = string.Empty; discPrice = string.Empty;
                }
            }
        }
        protected void ddlSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            url = OrginalUrl.Replace("sortBy=" + Request.QueryString["sortBy"], "sortBy=" + ddlSortBy.SelectedIndex);
            Response.Redirect(url);
        }

        protected void ddlColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            url = OrginalUrl.Replace("color=" + Request.QueryString["color"], "color=" + ddlColors.SelectedValue);
            Response.Redirect(url);
        }
    }
}