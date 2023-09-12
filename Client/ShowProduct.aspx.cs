using Store.Admin;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Store.Client
{
    public partial class ShowProduct : System.Web.UI.Page
    {
        ClsBasic clsBasic;
        DataTable dtDept, dtSize, dtProduct;
        string active = string.Empty, url, OrginalUrl;
        string whereDept, whereSize, whereColor, whereSortBy, whereSaerch;
        string All, Name, Size, Color, addToCart, Off, unitPrice, many , item;

        protected void Page_Load(object sender, EventArgs e)
        {
                OrginalUrl = HttpContext.Current.Request.Url.AbsoluteUri;
            if (!IsPostBack)
            {
                Name = Application["lang"].ToString() == "en" ? "NameEn" : "NameAr";
                Size = Application["lang"].ToString() == "en" ? "Size" : "القياس";
                Color = Application["lang"].ToString() == "en" ? "Color" : "اللون";
                addToCart = Application["lang"].ToString() == "en" ? "Add to cart" : "أضف إلى السلة";
                Off = Application["lang"].ToString() == "en" ? "OFF" : "خصم";
                All = Application["lang"].ToString() == "en" ? "All" : "الكل";
                many = Application["lang"].ToString() == "en" ? "KD" : "د.ك";
                item = Application["lang"].ToString() == "en" ? " ITEMS " : " العناصر ";


                ddlSortBy.Items.Clear();
                ddlSortBy.Items.Add(Application["lang"].ToString() == "en" ? "Default" : "افتراضي");
                ddlSortBy.Items.Add(Application["lang"].ToString() == "en" ? "name" : "الاسم");
                ddlSortBy.Items.Add(Application["lang"].ToString() == "en" ? "Price: low to high" : "السعر: من الأعلى الى الأقل");
                ddlSortBy.Items.Add(Application["lang"].ToString() == "en" ? "Price: high to low" : "السعر: من الأقل الى الأعلى");

                HtmlInputText txtSearch = Master.FindControl("txtSearch") as HtmlInputText;
                txtSearch.Value = Request.QueryString["search"] == "0" ? string.Empty : Request.QueryString["search"];
                ddlColors.DataBind();
                ddlColors.Items.Insert(0, new ListItem() { Value = "0", Text = "default" });
                ddlColors.SelectedValue = Request.QueryString["color"];
                ddlSortBy.SelectedIndex = int.Parse(Request.QueryString["sortBy"]);
                switch (ddlSortBy.SelectedIndex)
                {
                    case 0:
                        whereSortBy = " ORDER BY Id DESC";
                        break;
                    case 1:
                        whereSortBy = " ORDER BY " +Name;
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


                dtProduct = clsBasic.SelectData("Product.Id, NameAr, NameEn, Price, Discount, Color.Name as ColorName, Size.Name as SizeNum, Photo, Cnt", "Product INNER JOIN [Color] ON Color.Id = Product.CId INNER JOIN [Size] ON Size.Id = Product.SId WHERE Cnt > 0 " + whereDept + whereSize + whereColor + whereSaerch + whereSortBy);

                active = Request.QueryString["dept"] == "0" ? "text-[#b22234]" : "";
                url = OrginalUrl.Replace("dept=" + Request.QueryString["dept"], "dept=0");
                ltCategories.Text = string.Empty;
                ltCategories.Text += $@"
                    <li class=""px-1 py-2 cursor-pointer font-medium hover:text-[#b22234] capitalize {active}""><a href=""{url}"">{All}</a></li>
                ";
                foreach (DataRow row in dtDept.Rows)
                {
                    active = Request.QueryString["dept"] == row["Id"].ToString() ? "text-[#b22234]" : "";
                    url = OrginalUrl.Replace("dept=" + Request.QueryString["dept"], "dept=" + row["Id"]);
                    ltCategories.Text += $@"
                    <li class=""px-1 py-2 cursor-pointer font-medium hover:text-[#b22234] capitalize {active}""><a href=""{url}"">{row[Name]}</a></li>
                ";
                }

                active = Request.QueryString["size"] == "0" ? "activeSize" : "";
                url = OrginalUrl.Replace("size=" + Request.QueryString["size"], "size=0");
                ltSizes.Text = string.Empty;
                ltSizes.Text = $@"
                    <a href=""{url}"" class=""selectedItem  notActive px-2 py-2 rounded font-medium hover:text-[#b22234] capitalize {active}"">{All}</a>
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
                cntProduct.InnerText = dtProduct.Rows.Count + item;
                string disc = string.Empty, finalPrice = string.Empty, discPrice = string.Empty;
                foreach (DataRow row in dtProduct.Rows)
                {
                    finalPrice = row["Price"].ToString();
                    if (int.Parse(row["Discount"].ToString()) > 0)
                    {
                        disc = $@"<span
                            class=""absolute top-0 left-0 m-2 rounded-full bg-[#b22234] px-2 text-center text-sm font-medium text-white"">{100 - ((int.Parse(row["Discount"].ToString()) * 100) / int.Parse(row["Price"].ToString()))}% {Off}</span>";

                        discPrice = $@"<span class=""salePrice text-sm text-[#504f85] line-through uppercase"">{many}{row["Price"]}</span>";
                        finalPrice = row["Discount"].ToString();
                    }
                    ltProdcut.Text += $@"
                                        <div
                                            class=""productElement relative  flex w-full  flex-col overflow-hidden rounded-lg border border-gray-100 bg-white shadow-lg transition-all duration-300 hover:shadow-2xl hover:-translate-y-1 "" data-id=""{row["Id"]}"" data-count=""{row["Cnt"]}"">
                                            <a class=""relative mx-3 mt-3 flex h-60 overflow-hidden rounded-xl"" href=""DetailsProduct.aspx?id={row["Id"]}"" target=""_blank"">
                                                <img class=""imgProduct object-fill w-full h-full"" src=""data:image;base64,{Convert.ToBase64String((byte[])row["Photo"])}""
                                                    alt=""product image"" />
                                            {disc}
                                            </a>
                                            <div class=""mt-4 px-5 pb-5"">
                                                <a href=""DetailsProduct.aspx?id={row["Id"]}"" target=""_blank"">
                                                    <div>
                                                        <h5 class=""text-xl tracking-tight text-[#504f85] product-name"">{row[Name]}</h5>
                                                    </div>
                                                    <div class=""mt-2 mb-5 flex items-center justify-between"">
                                                        <p>
                                                            <span class=""text-xl font-normal text-[#504f85] product-price uppercase"">{many}<span>{finalPrice}</span>
                                                            </span>
                                                            {discPrice}
                                                        </p>

                                                    </div>
                                                <!-- size  color -->
                                                <div class=""flex justify-between mb-2"">
                                                    <h1 class=""capitalize color"">{Color}: <span class=""capitalize"">{row["ColorName"]}</span></h1>
                                                    <h1 class=""capitalize size"">{Size}: <span class=""capitalize"">{row["SizeNum"]}</span></h1>
                                                </div>
                                                </a>
                                                <!-- add to cart -->
                                                <div
                                                    class=""add-to-cart flex gap-2 items-center justify-center rounded-md bg-[#007a3d] cursor-pointer px-5 py-2.5 text-center text-sm font-medium text-white transition-all hover:bg-[#117a3d] focus:outline-none"">
                                                    <i class=""fa-solid fa-cart-shopping""></i>
                                                    {addToCart}
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