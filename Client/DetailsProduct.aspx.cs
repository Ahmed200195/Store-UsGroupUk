﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Store.Client
{
    public partial class DetailsProduct : Page
    {
        string Name, Size, Color, addToCart, Off, Info, unitPrice, many;
        DataRow dataRow;
        ClsBasic clsBasic;
        DataSet dataSet;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["lang"] == null)
            {
                Session["lang"] = "ar";
            }
            Page.Title = Session["lang"].ToString() == "en" ? "UsGroupUk | Product Details" : "UsGroupUk | تفاصيل المنتج";
            Name = Session["lang"].ToString() == "en" ? "NameEn" : "NameAr";
            Info = Session["lang"].ToString() == "en" ? "InfoEn" : "InfoAr";
            Size = Session["lang"].ToString() == "en" ? "Size" : "القياس";
            Color = Session["lang"].ToString() == "en" ? "Color" : "اللون";
            addToCart = Session["lang"].ToString() == "en" ? "Add to cart" : "أضف إلى السلة";
            Off = Session["lang"].ToString() == "en" ? "OFF" : "خصم";
            unitPrice = Session["lang"].ToString() == "en" ? "Unit Price" : "سعر الوحدة";
            many = Session["lang"].ToString() == "en" ? "KD" : "د.ك";

            string queries = $@"
                SELECT Product.Id, NameEn, NameAr, InfoEn, InfoAr, Size.Name as SizeName, Color.Name as ColorName, Photo, Price, Discount, DId FROM Product FULL OUTER JOIN Size ON Size.Id = SId FULL OUTER JOIN Color ON Color.Id = CId WHERE Product.Id = {Request.QueryString["id"]};
                SELECT * FROM ProductPhotos WHERE PId = {Request.QueryString["id"]};
                SELECT TOP 4 Product.Id, NameAr, NameEn, Price, Discount, Color.Name as ColorName, Size.Name as SizeNum, Photo, Cnt, DId FROM Product FULL OUTER JOIN [Color] ON Color.Id = Product.CId FULL OUTER JOIN [Size] ON Size.Id = Product.SId WHERE Cnt > 0 AND Product.Id IN (SELECT PId FROM LinkProduct WHERE Id IN (SELECT Id FROM InfoLinkProduct)) ORDER BY NEWID();
            ";
            clsBasic = new ClsBasic();
            dataSet = clsBasic.SelectMultiple(queries);
            
            if (dataSet.Tables[0].Rows.Count > 0)
            {
				dataRow = dataSet.Tables[0].Rows[0];
			}
            else
            {
                dataRow = clsBasic.SelectData("Product.Id, NameEn, NameAr, InfoEn, InfoAr, Photo, Price, Discount, DId", $"Product WHERE Product.Id = {Request.QueryString["id"]};").Rows[0];
			}

            ltImgSilder.Text = string.Empty;
            ltImgSilder.Text += $@"
                            <figure class=""product-banner min-w-full rounded-2xl"">
                                <img data-src=""../Uploads/Product/{dataRow["Photo"]}""  alt=""{dataRow[Name]}""
                                    class=""imgProduct img-cover w-full h-full rounded-2xl lazy-load""  />
                            </figure>
                ";
            foreach (DataRow row in dataSet.Tables[1].Rows)
            {
                ltImgSilder.Text += $@"
                            <figure class=""product-banner min-w-full rounded-2xl"">
                                <img data-src=""../Uploads/Product/{row["Name"]}""  alt=""{row["Name"]}""
                                    class=""img-cover w-full h-full rounded-2xl lazy-load""  />
                            </figure>
                ";
            }

            titleProduct.InnerText = dataRow[Name].ToString();
            InfoProdcut.InnerText = dataRow[Info].ToString();

            string color =string.Empty, size = string.Empty;
            if (dataRow.Table.Columns.Contains("ColorName"))
            {
				snColor.InnerText = dataRow["ColorName"].ToString();
			}
            else
            {
				titleColor.Visible= false;
			}
			if (dataRow.Table.Columns.Contains("SizeName"))
			{
				snSize.InnerText = dataRow["SizeName"].ToString();
			}
			else
			{
				titleSize.Visible = false;
			}

            string disc = string.Empty, finalPrice = string.Empty, discPrice = string.Empty;
            finalPrice = dataRow["Price"].ToString();
            if (int.Parse(dataRow["Discount"].ToString()) > 0)
            {
                disc = $@"<span class=""badge"">{100 - ((int.Parse(dataRow["Discount"].ToString()) * 100) / int.Parse(dataRow["Price"].ToString()))}%</span>";

                discPrice = $@"<del class=""del"">{many}{dataRow["Price"]}</del>";
                finalPrice = dataRow["Discount"].ToString();
            }
            else
            {
                finalPrice = dataRow["Price"].ToString();
            }
            ltPrice.Text = string.Empty;
            ltPrice.Text += $@"
                            <div class=""flex flex-col"">
                                <span class=""product-price text-gray-400 capitalize"">{unitPrice}: {many} <span class=""spanPrice"">{finalPrice}</span></span>
                                <span class=""price"" data-total-price>{many} <span>{finalPrice}</span> </span>
                            </div>
                            {disc}
                            {discPrice}
                ";

            ltFrequentlyProduct.Text = string.Empty;
            disc = string.Empty; finalPrice = string.Empty; discPrice = string.Empty;
            DataRow[] rowLinkProduct = dataSet.Tables[2].Select("DId = " + dataRow["DId"]);

            if (rowLinkProduct.Count() > 0)
            {
                dvLinkBuy.Visible = true;
                foreach (DataRow row in rowLinkProduct)
                {
                    finalPrice = row["Price"].ToString();
                    if (int.Parse(row["Discount"].ToString()) > 0)
                    {
                        disc = $@"<span
                            class=""absolute top-0 left-0 m-2 rounded-full bg-[#b22234] px-2 text-center text-sm font-medium text-white"">{100 - ((int.Parse(row["Discount"].ToString()) * 100) / int.Parse(row["Price"].ToString()))}% {Off}</span>";

                        discPrice = $@"<span class=""salePrice text-sm text-[#504f85] line-through uppercase"">{many}{row["Price"]}</span>";
                        finalPrice = row["Discount"].ToString();
                    }
                    ltFrequentlyProduct.Text += $@"
                    <div class=""carousel-cell md:w-2/4 w-full"">
                        <div
                            class=""productElement  relative  flex flex-col w-[95%] overflow-hidden rounded-lg border border-gray-100 bg-white shadow-lg transition-all duration-300 hover:shadow-2xl hover:-translate-y-1 "" data-id=""{row["Id"]}"" data-count=""{row["Cnt"]}"">
                            <a class=""relative mx-3 mt-3 flex h-60 overflow-hidden rounded-xl"" href=""DetailsProduct.aspx?id={row["Id"]}&cnt={row["Cnt"]}"">
                                <img class=""imgProduct object-fill w-full h-full lazy-load"" data-src=""../Uploads/Product/{row["Photo"]}""
                                    alt=""product image""  />
                                {disc}
                            </a>
                            <div class=""mt-4 px-5 pb-5"">
                                <a href=""DetailsProduct.aspx?id={row["Id"]}&cnt={row["Cnt"]}"" >
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
                                <div
                                    class=""add-to-cart flex gap-2 items-center justify-center rounded-md bg-[#007a3d] cursor-pointer px-5 py-2.5 text-center text-sm font-medium text-white transition-all hover:bg-[#117a3d] focus:outline-none"">
                                    <i class=""fa-solid fa-cart-shopping""></i>
                                    {addToCart}
                                </div>
                            </div>
                        </div>
                    </div>
                ";
                }
            }
            else
            {
                dvLinkBuy.Visible = false;
            }
        }
    }
}