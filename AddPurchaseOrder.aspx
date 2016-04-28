<%@ Page Title="" Language="C#" MasterPageFile="~/PurchaseMaster.master" AutoEventWireup="true" CodeFile="AddPurchaseOrder.aspx.cs" Inherits="AddPurchaseOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <script src="JavaScripts/Purchase.js"></script>
                <table width="980" border="0" align="right" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="height: 21px">
                            <div align="left">

                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td style="width:1000px;"><span>
                                            <img src="img/Logo_Small.png" />
                                        </span>
                                            <br />
                                            <span style="font-size: 25px; font-weight:bolder; padding: 0px 105px;"  lang="EN-US" xml:lang="EN-US">THE KALGIDHAR TRUST </span>
                                            <br />
                                            <br />
                                            <span style="font-size: 15px; padding: 0px 138px;"><i>Service to Humanity</i></span><br />
                                            <br />
                                        </td>
                                        <td></td>
                                        <td style="width:60px;" valign="top">
                                            <span style="font-weight: bold; font-size: 15px;">PURCHASEORDER</span>
                                        </td>
                                    </tr>
                                 </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span style="font-size: 15px;">The Kalgidhar Trust,Baru Sahib,Via Rajgarh<br />
                                Distt.sirmour,Himachal Pradesh-173101<br />
                                Phone 91-1799-276031 Fax 91-1799-276041
                            </span>
                        </td>
                        <td>
                            <span style="font-weight: bold; padding-right: 317px;">P.O.</span>
                            <label id="lblpo" style=" font-weight:bold"></label>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <span style="font-weight: bold; padding-right: 317px;">Date:</span>
                            <label id="lblCurrentDate" style=" font-weight:bold"></label>
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                </table>
         
       
                <table width="1000" border="1" align="right" cellspacing="0" bordercolor="#000000" cellpadding="0">
                    <tr>
                        <td style="width: 2px; text-align: center; vertical-align: middle;">
                            <div style="height: 50px; width: 12px;">
                             <select id="drpVendor" style="width:115px;"><option value="0">Select Vendor</option></select></div>
                           
                        </td>
                        <td style="width: 40px;">
                            <div style="height: 50px;"><span>M/S</span></div>
                            <label id="lblvendor"></label>
                        </td>
                        <td style="width: 2px; text-align: center; vertical-align: middle;">
                            <div style="height: 50px; width: 7px;"><span>SHIP TO</span></div>
                        </td>
                        <td style="width: 45px;">
                            <div style="height: 50px;"><span></span></div>
                        </td>
                    </tr>
                </table>
                <table width="1000" border="1" height="15" align="right"></table>
       
                <table width="1000" border="1" align="right" cellpadding="0" cellspacing="0" bordercolor="#000000">
                    <tr>
                        <td colspan="3" style="width: 35px; background-color: #CCCCCC; text-align: center; vertical-align: middle;">
                            <div><span class="style15">SHIPPING METHOD</span></div>
                        </td>
                        <td style="width: 30px; background-color: #CCCCCC; text-align: center; vertical-align: middle;">
                            <div><span class="style15">SHIPPING TERMS</span></div>
                        </td>
                        <td style="width: 35px; background-color: #CCCCCC; text-align: center; vertical-align: middle;" colspan="2">
                            <div><span class="style15">DELIVERY DATE</span></div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="width: 35px; text-align: center; vertical-align: middle;">
                            <div><span class="style15">By Road</span></div>
                        </td>
                        <td style="width: 30px;"></td>
                        <td colspan="2" style="width: 35px; text-align: center; vertical-align: middle;">
                            <div><span class="style15"></span></div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;">
                            <div><span class="style15">QTY</span></div>
                        </td>
                        <td style="width: 10px; background-color: #CCCCCC; text-align: center; vertical-align: middle;">
                            <div><span class="style15">Sr.No</span></div>
                        </td>
                        <td style="width: 15px; background-color: #CCCCCC; text-align: center; vertical-align: middle;">
                            <div><span class="style15">DESCRIPTION</span></div>
                        </td>
                        <td style="width: 30px; background-color: #CCCCCC; text-align: center; vertical-align: middle;">
                            <div><span class="style15">DETAIL</span></div>
                        </td>
                        <td style="width: 20px; height: 19px; background-color: #CCCCCC; text-align: center; vertical-align: middle;">
                            <div><span class="style11">UNIT PRICE</span></div>
                        </td>
                        <td style="width: 15px; background-color: #CCCCCC; text-align: center; vertical-align: middle;">
                            <div><span class="style11">LINE TOTAL</span></div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10px;"></td>
                        <td style="width: 10px; text-align: center; vertical-align: middle;">
                            <div><span class="style15">1</span></div>
                        </td>
                        <td style="width: 15px;"></td>
                        <td style="width: 30px;"></td>
                        <td style="width: 20px; height: 19px;"></td>
                        <td style="width: 15px;"></td>
                    </tr>
                    <tr>
                        <td style="width: 10px;"></td>
                        <td style="width: 10px; text-align: center; vertical-align: middle;">
                            <div><span class="style15">2</span></div>
                        </td>
                        <td style="width: 15px;"></td>
                        <td style="width: 30px;"></td>
                        <td style="width: 20px; height: 19px;"></td>
                        <td style="width: 15px;"></td>
                    </tr>
                    <tr>
                        <td style="width: 10px;"></td>
                        <td style="width: 10px; text-align: center; vertical-align: middle;">
                            <div><span class="style15">3</span></div>
                        </td>
                        <td style="width: 15px;"></td>
                        <td style="width: 30px;"></td>
                        <td style="width: 20px; height: 19px;"></td>
                        <td style="width: 15px;"></td>
                    </tr>
                    <tr>
                        <td style="width: 10px;"></td>
                        <td style="width: 10px;">
                            <div><span class="style15"></span></div>
                        </td>
                        <td style="width: 15px;"></td>
                        <td style="width: 30px;"></td>
                        <td style="width: 20px; height: 19px;"></td>
                        <td style="width: 15px;"></td>
                    </tr>
                    <tr>
                        <td rowspan="3" style="width: 65px;" colspan="4">
                            <div><span style="font-weight: bolder; font-size: 14px; color: black; font-family: Arial"><u>BILLING ADDRESS:-</u></span></div>
                            <br />
                            <div><span style="font-weight: bolder; font-size: 12px; color: black; font-family: Arial"><u>Contact Person:-</u></span></div>
                        </td>
                        <td style="width: 20px; text-align: center; font-weight: bolder; vertical-align: middle;">
                            <div><span class="style11">SUBTOTAL</span></div>
                        </td>
                        <td style="width: 15px;">
                            <div><span class="style11"></span></div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px; text-align: center; vertical-align: middle;">
                            <div><span class="style11">VAT</span></div>
                        </td>
                        <td style="width: 15px;">
                            <div><span class="style11"></span></div>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 20px; text-align: center; font-weight: bolder; vertical-align: middle;">
                            <div><span class="style11">TOTAL</span></div>
                        </td>
                        <td style="width: 15px;">
                            <div><span class="style11"></span></div>
                        </td>
                    </tr>
                </table>
                <table width="1000" border="0" align="right">
                    <tr>
                        <td style="text-align: center; vertical-align: middle;">
                            <strong><span style="font-size: 12pt; font-weight: bold; font-family: Arial"><u>Please read the instruction carefully</u></span></strong>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 71px; vertical-align: top;" rowspan="2"><span class="style13" style="width: 72px; height: 18px;"></span>

                            <span style="display: inline-block; height: 13px; width: 816px;">1. Please ensure to sendthe Original Bill to THE KALGIDHAR TRUST C-120, INDUSTRIAL AREA PHASE-8, MOHALI-160071 as per instructions.</span><br />
                            <span style="width: 72px; height: 18px">2. Material should be sent at the destination with Original Bill Only</span><br />
                            <span style="width: 72px; height: 17px;">3.Please send two copies of invoice with material</span><br />
                            <span style="width: 72px; height: 18px">4. Please treat this order in accordance with prices,terms,delivery method and specification listed above.</span><br />
                            <span style="display: inline-block; height: 13px; width: 816px;">5. Please notify us immediately if you are unable to supply the material beyond your control. A penalty @1% per week will be charged for delayed supply of material at destination.</span><br />
                            <span style="width: 72px; height: 17px;">6. Send all correspondence to : MOHALI OFFICE. E-MAIL mohali@barusahib.org <br />Office Contact No:0172-5094200</span><br />
                            <span style="width: 72px; height: 17px;">7.Delivery must be completed by .......</span><br />
                            <span style="width: 72px; height: 17px;"><u>8.please acknowledge the receipt of Purchase Order in person or by mail:</u></span>
                            <ul>
                                <li><span>Vat/CST:-</span></li>
                                <li><span>Sale Tax:-</span></li>
                                <li><span>Excise Duty:</span></li>
                                <li><span>Labour Charges:-</span></li>
                                <li><span>Freight:-</span></li>
                                <li><span>Mode Of Dispatch:- By Road</span></li>
                                <li><span>Out ST Reg no:- Not Required</span></li>
                                <li><span>Payment:-</span></li>
                            </ul>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="font-weight: bolder; font-size: 14px; width: 300px; color: black; font-family: Arial">Authorised By</td>
                    </tr>
                </table>
           
 
</asp:Content>

