using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Store.Client
{
    public partial class DetailsProduct : Page
    {
        DataRow dataRow;
        ClsBasic clsBasic;
        protected void Page_Load(object sender, EventArgs e)
        {
            clsBasic = new ClsBasic();
            dataRow = clsBasic.SelectData("*", "Product WHERE Id = " + Request.QueryString["id"]).Rows[0];
            ltImgSilder.Text = string.Empty;
            ltImgSilder.Text += $@"
                            <figure class=""product-banner min-w-full rounded-2xl"">
                                <img src=""data:image;base64,{Convert.ToBase64String((byte[])dataRow["Photo"])}"" loading=""lazy"" alt=""{dataRow["NameEn"]}""
                                    class=""imgProduct img-cover w-full h-full rounded-2xl"" />
                            </figure>
                ";
            foreach (DataRow row in clsBasic.SelectData("*", "ProductPhotos WHERE PId = " + Request.QueryString["id"]).Rows)
            {
                ltImgSilder.Text += $@"
                            <figure class=""product-banner min-w-full rounded-2xl"">
                                <img src=""data:image;base64,{Convert.ToBase64String((byte[])row["Photo"])}"" loading=""lazy"" alt=""{row["Name"]}""
                                    class=""img-cover w-full h-full rounded-2xl"" />
                            </figure>
                ";
            }

            titleProduct.InnerText = dataRow["NameEn"].ToString();
            InfoProdcut.InnerText = dataRow["InfoEn"].ToString();

            ltSizes.Text = string.Empty;
            foreach (DataRow row in clsBasic.SelectData("*", "Size").Rows)
            {
                ltSizes.Text += $@"
                    <p class=""selectedItem border-2 border-[#3c3b6e] py-2 px-4 rounded cursor-pointer"">
                          {row["Name"]}
                      </p>
                ";
            }
            
            string disc = string.Empty, finalPrice = string.Empty, discPrice = string.Empty;
            finalPrice = dataRow["Price"].ToString();
            if (int.Parse(dataRow["Discount"].ToString()) > 0)
            {
                disc = $@"<span class=""badge"">{100 - ((int.Parse(dataRow["Discount"].ToString()) * 100) / int.Parse(dataRow["Price"].ToString()))}%</span>";

                discPrice = $@"<del class=""del"">${dataRow["Price"]}</del>";
                finalPrice = dataRow["Discount"].ToString();
            }
            else
            {
                finalPrice = dataRow["Price"].ToString();
            }
            ltPrice.Text = string.Empty;
            ltPrice.Text += $@"
                            <div class=""flex flex-col"">
                                <span class=""product-price text-gray-400 capitalize"">unit price: $ <span class=""spanPrice"">{finalPrice}</span></span>
                                <span class=""price"" data-total-price>$ <span>{finalPrice}</span> </span>
                            </div>
                            {disc}
                            {discPrice}
                ";

            ltFrequentlyProduct.Text = string.Empty;
            disc = string.Empty; finalPrice = string.Empty; discPrice = string.Empty;
            foreach (DataRow row in clsBasic.SelectData("*", "Product").Rows)
            {
                finalPrice = row["Price"].ToString();
                if (int.Parse(row["Discount"].ToString()) > 0)
                {
                    disc = $@"<span
                            class=""absolute top-0 left-0 m-2 rounded-full bg-[#b22234] px-2 text-center text-sm font-medium text-white"">{100 - ((int.Parse(row["Discount"].ToString()) * 100) / int.Parse(row["Price"].ToString()))}% OFF</span>";

                    discPrice = $@"<span class=""salePrice text-sm text-[#504f85] line-through uppercase"">${row["Price"]}</span>";
                    finalPrice = row["Discount"].ToString();
                }
                ltFrequentlyProduct.Text += $@"
                    <div class=""carousel-cell w-2/4"">
                        <div
                            class=""productElement  relative  flex flex-col w-[95%] overflow-hidden rounded-lg border border-gray-100 bg-white shadow-lg transition-all duration-300 hover:shadow-2xl hover:-translate-y-1 "">
                            <a class=""relative mx-3 mt-3 flex h-60 overflow-hidden rounded-xl"" href=""DetailsProduct.aspx?id={row["Id"]}"">
                                <img class=""imgProduct object-fill w-full h-full"" src=""data:image;base64,{Convert.ToBase64String((byte[])row["Photo"])}""
                                    alt=""product image"" />
                                {disc}
                            </a>
                            <div class=""mt-4 px-5 pb-5"">
                                <a href=""DetailsProduct.aspx?id={row["Id"]}"">
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
                                <div
                                    class=""add-to-cart flex gap-2 items-center justify-center rounded-md bg-[#007a3d] cursor-pointer px-5 py-2.5 text-center text-sm font-medium text-white transition-all hover:bg-[#117a3d] focus:outline-none"">
                                    <i class=""fa-solid fa-cart-shopping""></i>
                                    Add to cart
                                </div>
                            </div>
                        </div>
                    </div>
                ";
            }
        }
    }
}