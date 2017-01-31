using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace MLAW_Order_System
{

    //Makes all requests to the database for the app
    public class DAL
    {
        String strConn = "Server=mlawdb.cja22lachoyz.us-west-2.rds.amazonaws.com;Database=MLAW_MS;User Id=sa;Password=!sd2the2power!;";

        public DataSet doLogin(String strEmail, String strPassword)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Do_Login", conn);
                sqlComm.Parameters.AddWithValue("@Email", strEmail);
                sqlComm.Parameters.AddWithValue("@Password", strPassword);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getClientFoundationOrders(int iClient_Id)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Client_Foundation_Orders", conn);
                sqlComm.Parameters.AddWithValue("@Client_Id", iClient_Id);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }
            return (ds);
        }

        public DataSet getClientQuickSearch(String strSearch, int iClient_Id)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Do_Customer_Quick_Search", conn);
                sqlComm.Parameters.AddWithValue("@Address", strSearch);
                sqlComm.Parameters.AddWithValue("@Client_Id", iClient_Id);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }
            return (ds);
        }

        public DataSet getClientPortalInfo(int iClient_Id)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Client_Portal_info", conn);
                sqlComm.Parameters.AddWithValue("@Client_Id", iClient_Id);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }
            return (ds);
        }

        public DataSet doCustomerLogin(String strLogin, String strPassword)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Do_Client_Login", conn);
                sqlComm.Parameters.AddWithValue("@Login", strLogin);
                sqlComm.Parameters.AddWithValue("@Password", strPassword);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }
        public DataSet getOrdersByStatus(String strStatuses)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Orders_By_Status", conn);
                sqlComm.Parameters.AddWithValue("@Status_List", strStatuses);

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public void removeClientMailDelivery(int iClientMailId)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Remove_Client_Delivery_Email", conn);
                sqlComm.Parameters.AddWithValue("@Client_Delivery_Email_Id", iClientMailId);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }
           
        }

        public void updateClientPortalLogin(int iClientId, String strUsername, String strPassword)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Upsert_Client_Portal_Login", conn);
                sqlComm.Parameters.AddWithValue("@Client_Id", iClientId);
                sqlComm.Parameters.AddWithValue("@Login", strUsername);
                sqlComm.Parameters.AddWithValue("@Password", strPassword);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }

        }

        public void insertClientDeliveryEmail(int iClientId, string strEmail)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Insert_Client_Delivery_Email", conn);
                sqlComm.Parameters.AddWithValue("@Client_Id", iClientId);
                sqlComm.Parameters.AddWithValue("@Email", strEmail);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }

        }

        public DataSet getClientEmails(int iClientID)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Client_Delivery_Emails", conn);
                sqlComm.Parameters.AddWithValue("@Client_Id", iClientID);

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getInspectorInfo(int iInspectorId)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Inspector_Info", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@Inspector_Id", iInspectorId);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getFoundationOrderPrice(int iClientId, int iSqFt)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Foundation_Order_Price", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@Client_Id", iClientId);
                sqlComm.Parameters.AddWithValue("@Sq_Ft_Threshold", iSqFt);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getInspectionOrderPrice(int iClientId, int iInspectionTypeId)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Inspection_Order_Price", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@Client_Id", iClientId);
                sqlComm.Parameters.AddWithValue("@Inspection_Type_Id", iInspectionTypeId);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getInspectorInspectionTypes(int iInspectorId)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Inspector_Inspection_Types", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@Inspector_Id", iInspectorId);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getTodaysOrders()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Todays_Inspection_Orders", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }


        public DataSet getOrderSlice(String strStatuses, int iDateType, String strStartDate, String strEndDate)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Order_Slice", conn);
                sqlComm.Parameters.AddWithValue("@Status_List", strStatuses);
                sqlComm.Parameters.AddWithValue("@DateType", iDateType);
                sqlComm.Parameters.AddWithValue("@StartDate", strStartDate);
                sqlComm.Parameters.AddWithValue("@EndDate", strEndDate);
                
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet get800OrderSlice(String strStatuses, int iDateType, String strStartDate, String strEndDate)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_800_Order_Slice", conn);
                sqlComm.Parameters.AddWithValue("@StatusList", strStatuses);
                sqlComm.Parameters.AddWithValue("@DateType", iDateType);
                sqlComm.Parameters.AddWithValue("@StartDate", strStartDate);
                sqlComm.Parameters.AddWithValue("@EndDate", strEndDate);

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getWarrantyOrderSlice(String strStartDate, String strEndDate)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Warranty_Order_Slice", conn);
                sqlComm.Parameters.AddWithValue("@StartDate", strStartDate);
                sqlComm.Parameters.AddWithValue("@EndDate", strEndDate);

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }


        public DataSet getOrderByMLAWNumber(String strMLAWNumber)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Order_By_MLAW_Number", conn);
                sqlComm.Parameters.AddWithValue("@MLAW_Number", strMLAWNumber);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getAllInspectionTypes()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_All_Inspection_Types", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public void UpdateInspectionType(int iInspectionTypeId, int iCompletionTime)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Update_Inspection_Type", conn);
                sqlComm.Parameters.AddWithValue("@Inspection_Type_Id", iInspectionTypeId);
                sqlComm.Parameters.AddWithValue("@Completion_Time", iCompletionTime);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }
        }

        public DataSet getOrderInfo(int iOrderId)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Order_By_Id", conn);
                sqlComm.Parameters.AddWithValue("@Order_Id", iOrderId);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getOrderFiles(int iOrderId)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Order_Files_Info", conn);
                sqlComm.Parameters.AddWithValue("@Order_Id", iOrderId);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getOrderFile(int iOrderFileId)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Order_File", conn);
                sqlComm.Parameters.AddWithValue("@Order_File_Id", iOrderFileId);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }


        public DataSet getUserLevels()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_User_Levels", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getFoundationStatuses()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_All_Order_Statuses", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }


        public DataSet getOrderRevisions(int iOrderId)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Order_Revisions", conn);
                sqlComm.Parameters.AddWithValue("@Order_Id", iOrderId);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getInspectionCompletedOrders(DateTime dtStartDate, DateTime dtEndDate)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Inspection_Completed_Orders", conn);
                sqlComm.Parameters.AddWithValue("@StartDate", dtStartDate);
                sqlComm.Parameters.AddWithValue("@EndDate", dtEndDate);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }


        public DataSet getCHRTCompletedOrders(DateTime dtStartDate, DateTime dtEndDate)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_CHRT_Completed_Orders", conn);
                sqlComm.Parameters.AddWithValue("@StartDate", dtStartDate);
                sqlComm.Parameters.AddWithValue("@EndDate", dtEndDate);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getChrtDesignerOpenOrders()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_CHRT_Designer_Open_Orders", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getChrtOpenOrders()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_CHRT_Open_Orders", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getOpenRevisions()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Open_Revisions", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }


        public DataSet getChrtOpenOrdersLate()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_CHRT_Open_Orders_Late", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getChrtOpenOrdersDueToday()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_CHRT_Open_Orders_Tomorrow", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getChrtOpenOrdersOnTime()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_CHRT_Open_Orders_On_Time", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getChrtOpenOrdersHold()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_CHRT_Open_Orders_Hold", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }



        public DataSet getChrtOpenOrdersByStatus()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_CHRT_Open_Orders_By_Status", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }


        public DataSet doQuickSearch(String strAddress)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Do_Quick_Search", conn);
                sqlComm.Parameters.AddWithValue("@Address", strAddress);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet findMLAWNumber(String strAddress, String strLot, String strBlock)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Find_MLAW_Num", conn);
                sqlComm.Parameters.AddWithValue("@Address", strAddress);
                sqlComm.Parameters.AddWithValue("@Lot", strLot);
                sqlComm.Parameters.AddWithValue("@Block", strBlock);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getOpenOrders(int? iClient_Id)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Open_Orders", conn);
                sqlComm.Parameters.AddWithValue("@Client_Id", iClient_Id == null ? (object)DBNull.Value : iClient_Id);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getUnassignedOrders()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Unassigned_Orders", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getUserRevisions(int iUser_Id)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_User_Revisions", conn);
                sqlComm.Parameters.AddWithValue("@User_Id", iUser_Id);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getUserOrders(int iUser_Id)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_User_Orders", conn);
                sqlComm.Parameters.AddWithValue("@User_Id", iUser_Id);
                sqlComm.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getAllUsers()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_All_Users", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getActiveClients()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Active_Clients", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public void updateClientActiveStatus(int iClientId, int iStatusId)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Update_Client_Status", conn);
                sqlComm.Parameters.AddWithValue("@Client_Id", iClientId);
                sqlComm.Parameters.AddWithValue("@Status_Id", iStatusId);
                sqlComm.CommandType = CommandType.StoredProcedure;

                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }

        }

        public DataSet getActiveSubdivisions()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Active_Subdivisions", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }


        public DataSet getAllClients()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_All_Clients", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getAllDivisions()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_All_Divisions", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getFoundationDesigners()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Foundation_Designers", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getOrderHistory(int iOrder_Id)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Order_History", conn);
                sqlComm.Parameters.AddWithValue("@Order_Id", iOrder_Id);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getClientSubdivisions(int iClient_Id)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Client_Subdivisions", conn);
                sqlComm.Parameters.AddWithValue("@Client_Id", iClient_Id);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getAllSubdivisions()
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_All_Subdivisions", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }


        public DataSet getRevisionCount(string strMLAWNumber)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Revision_Number ", conn);
                sqlComm.Parameters.AddWithValue("@MLAW_Number", strMLAWNumber);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getClientByName(string strClientName)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Client_By_Name", conn);
                sqlComm.Parameters.AddWithValue("@Name", strClientName);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getClientById(int iClientId)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Client_By_Id", conn);
                sqlComm.Parameters.AddWithValue("@ID", iClientId);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }

        public DataSet getClientFoundationRevisionPricing(int iClientId)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Client_Foundation_Revision_Pricing", conn);
                sqlComm.Parameters.AddWithValue("@Client_id", iClientId);
                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);
        }


        public void updateUser(String strEmail, int iUserLevelId, String strPassword, String strFirstName, String strLastName, int iClientId, int iUserId, int iActive)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Update_User", conn);
                sqlComm.Parameters.AddWithValue("@Email", strEmail);
                sqlComm.Parameters.AddWithValue("@User_Level_Id", iUserLevelId);
                sqlComm.Parameters.AddWithValue("@Password", strPassword);
                sqlComm.Parameters.AddWithValue("@FirstName", strFirstName);
                sqlComm.Parameters.AddWithValue("@LastName", strLastName);
                sqlComm.Parameters.AddWithValue("@Client_Id", iClientId);
                sqlComm.Parameters.AddWithValue("@User_Id", iUserId);
                sqlComm.Parameters.AddWithValue("@Active", iActive);

                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }
        }


        public void updateRevision(int iOrderId, int iOrderStatusId, int iDesignerId, string strComments)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Update_Revision", conn);
                sqlComm.Parameters.AddWithValue("@Order_Id", iOrderId);
                sqlComm.Parameters.AddWithValue("@Order_Status_Id", iOrderStatusId);
                sqlComm.Parameters.AddWithValue("@Designer_Id", iDesignerId);
                sqlComm.Parameters.AddWithValue("@Revision_Text", strComments);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }

        }

        
        public DataSet insertUser(String strFirstName, String strLastName, String strEmail, String strPassword, int iUserLevelId, int iClientId)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Insert_User", conn);
                sqlComm.Parameters.AddWithValue("@First_Name", strFirstName);
                sqlComm.Parameters.AddWithValue("@Last_Name", strLastName);
                sqlComm.Parameters.AddWithValue("@Email", strEmail);
                sqlComm.Parameters.AddWithValue("@Password", strPassword);
                sqlComm.Parameters.AddWithValue("@User_Level_Id", iUserLevelId);
                sqlComm.Parameters.AddWithValue("@Client_Id", iClientId);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                DataSet ds = new DataSet();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
                return (ds);
            }
         
        }

        public void deleteOrder(int iOrderId)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Delete_Order", conn);
                sqlComm.Parameters.AddWithValue("@Order_Id", iOrderId);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }

        }

        public void updateFoundationOrderStatus(int iOrderId, int iStatusId)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Update_Order_Status", conn);
                sqlComm.Parameters.AddWithValue("@Order_Id", iOrderId);
                sqlComm.Parameters.AddWithValue("@Status_Id", iStatusId);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void assignOrder(int iOrderId, int iUserId)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Assign_Order", conn);
                sqlComm.Parameters.AddWithValue("@Order_Id", iOrderId);
                sqlComm.Parameters.AddWithValue("@User_Id", iUserId);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }
        }

        public DataSet insertRevision(int iOrder_Id, String strRevisionText, DateTime dtReceivedDate, String strPlanName, String strPlanNumber, Double dAmount)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Insert_Revision", conn);
                sqlComm.Parameters.AddWithValue("@Order_Id", iOrder_Id);
                sqlComm.Parameters.AddWithValue("@Revision_Text", strRevisionText);
                sqlComm.Parameters.AddWithValue("@ReceivedDate", dtReceivedDate);
                sqlComm.Parameters.AddWithValue("@Plan_Name", strPlanName);
                sqlComm.Parameters.AddWithValue("@Plan_Number", strPlanNumber);
                sqlComm.Parameters.AddWithValue("@Amount", dAmount);
                
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
               
            }
            return (ds);
        }


        public DataSet insertSubdivision(int iClient_Id, int iDivision_Id, String strSubdivision_Name, int iSubdivisionNumber)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Insert_Subdivision", conn);
                sqlComm.Parameters.AddWithValue("@Client_Id", iClient_Id);
                sqlComm.Parameters.AddWithValue("@Division_Id", iDivision_Id);
                sqlComm.Parameters.AddWithValue("@Subdivision_Name", strSubdivision_Name);
                sqlComm.Parameters.AddWithValue("@Subdivision_Number", iSubdivisionNumber);
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }
            return (ds);
        }

        public void updateOrder(int iOrder_id, String strAddress, String strCity, String strState, String strZip, int? iSubdivision_Id, DateTime dtDateReceived, DateTime dtDateDue,
    int iOrderStatusId, int? iWarrantyId, int iOrderTypeId, String strPlanNumber, String strPlanName, String strLot, String strBlock,
    String strSection, int iDivisionId, String strPI, DateTime? dtGeotecDate, String strSoilsDataSource, int? iFillApplied, String strSlope,
    Decimal? dSlabSquareFeet, Decimal? dEm_ctr, Decimal? dEm_edg, Decimal? dYm_ctr, Decimal? dYm_edg, Decimal? dBrg_cap, int? iSF, String strComments,
            String strElevation, String strContact, String strPhone, String strFoundationType, String strPhase, String strGarageOptions, String strPatioOptions, int iMasonrySides)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {

                SqlCommand sqlComm = new SqlCommand("Update_Order", conn);
                sqlComm.Parameters.AddWithValue("@Order_Id", iOrder_id);
                sqlComm.Parameters.AddWithValue("@Address", strAddress);
                sqlComm.Parameters.AddWithValue("@City", strCity);
                sqlComm.Parameters.AddWithValue("@State", strState);
                sqlComm.Parameters.AddWithValue("@Zip", strZip);
                sqlComm.Parameters.AddWithValue("@Subdivision_Id", iSubdivision_Id == null ? (object)DBNull.Value : iSubdivision_Id);
                sqlComm.Parameters.AddWithValue("@Date_Received", dtDateReceived);
                sqlComm.Parameters.AddWithValue("@Date_Due", dtDateDue);
                sqlComm.Parameters.AddWithValue("@Order_Status_Id", iOrderStatusId);
                sqlComm.Parameters.AddWithValue("@Order_Warranty_Id", iWarrantyId == null ? (object)DBNull.Value : iWarrantyId);
                sqlComm.Parameters.AddWithValue("@Order_Type_Id", iOrderTypeId);
                sqlComm.Parameters.AddWithValue("@Plan_Number", strPlanNumber);
                sqlComm.Parameters.AddWithValue("@Plan_Name", strPlanName);
                sqlComm.Parameters.AddWithValue("@Lot", strLot);
                sqlComm.Parameters.AddWithValue("@Block", strBlock);
                sqlComm.Parameters.AddWithValue("@Section", strSection);
                sqlComm.Parameters.AddWithValue("@PI", strPI);
                sqlComm.Parameters.AddWithValue("@Visual_Geotec_Date", dtGeotecDate == null ? (object)DBNull.Value : dtGeotecDate);
                sqlComm.Parameters.AddWithValue("@Soils_Data_Source", strSoilsDataSource);
                sqlComm.Parameters.AddWithValue("@Fill_Applied", iFillApplied == null ? (object)DBNull.Value : iFillApplied);
                sqlComm.Parameters.AddWithValue("@Slope", strSlope);
                sqlComm.Parameters.AddWithValue("@Slab_Square_Feet", dSlabSquareFeet == null ? (object)DBNull.Value : dSlabSquareFeet);
                sqlComm.Parameters.AddWithValue("@Em_ctr", dEm_ctr == null ? (object)DBNull.Value : dEm_ctr);
                sqlComm.Parameters.AddWithValue("@Em_edg", dEm_edg == null ? (object)DBNull.Value : dEm_edg);
                sqlComm.Parameters.AddWithValue("@Ym_ctr", dYm_ctr == null ? (object)DBNull.Value : dYm_ctr);
                sqlComm.Parameters.AddWithValue("@Ym_edg", dYm_edg == null ? (object)DBNull.Value : dYm_edg);
                sqlComm.Parameters.AddWithValue("@Brg_cap", dBrg_cap == null ? (object)DBNull.Value : dBrg_cap);
                sqlComm.Parameters.AddWithValue("@SF", iSF == null ? (object)DBNull.Value : iSF);
                sqlComm.Parameters.AddWithValue("@Comments", strComments);
                sqlComm.Parameters.AddWithValue("@Elevation", strElevation);
                sqlComm.Parameters.AddWithValue("@Contact", strContact);
                sqlComm.Parameters.AddWithValue("@Phone", strPhone);
                sqlComm.Parameters.AddWithValue("@Foundation_Type", strFoundationType);
                sqlComm.Parameters.AddWithValue("@Phase", strPhase);
                sqlComm.Parameters.AddWithValue("@Garage_Options", strGarageOptions);
                sqlComm.Parameters.AddWithValue("@Patio_Options", strPatioOptions);
                sqlComm.Parameters.AddWithValue("@Masonry_Sides", iMasonrySides);
                

                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }

        }

        public void updateDesignerStatus(int iOrderId, int iStatusId, String strComments)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Update_Designer_Status", conn);
                sqlComm.Parameters.AddWithValue("@Order_Id", iOrderId);
                sqlComm.Parameters.AddWithValue("@Status_Id", iStatusId);
                sqlComm.Parameters.AddWithValue("@Comments", strComments);

                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }

        }


        public void updateSubdivision(int iSubdivisionId, String strSubdivisionName, int iSubdivisionNumber, int iDivisionId)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Update_Subdivision", conn);
                sqlComm.Parameters.AddWithValue("@Subdivision_Id", iSubdivisionId);
                sqlComm.Parameters.AddWithValue("@Subdivision_Name", strSubdivisionName);
                sqlComm.Parameters.AddWithValue("@Subdivision_Number", iSubdivisionNumber);
                sqlComm.Parameters.AddWithValue("@Division_Id", iDivisionId);


                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }

        }


        public DataSet insertOrder(String strMLAWNumber, String strAddress, String strCity, String strState, String strZip, int? iSubdivision_Id, DateTime dtDateReceived,
            int iOrderStatusId, int? iWarrantyId, int iOrderTypeId, String strPlanNumber, String strPlanName, String strLot, String strBlock,
            String strSection, String strPI, DateTime? dtGeotecDate, String strSoilsDataSource, int? iFillApplied, String strSlope,
            Decimal? dSlabSquareFeet, Decimal? dEm_ctr, Decimal? dEm_edg, Decimal? dYm_ctr, Decimal? dYm_edg, Decimal? dBrg_cap, int? iSF, String strComments,
            String strElevation, String strContact, String strPhone, String strFoundationType, String strPhase, String strCounty, String strIsRevision, String strGarageType, String strPatio, String strFireplace,
            String strGarageOptions, String strPatioOptions, int iMasonrySides, String strFillDepth, String strSoilsComments, String strCustomerJobNumber, int? iParentId, Double dAmount, Double dDiscount, int iFoundationOrderType, int? iClientId, int? iDivisionId)
        {
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(strConn))
            {


                SqlCommand sqlComm = new SqlCommand("Insert_Order", conn);
               
                sqlComm.Parameters.AddWithValue("@MLAW_Number", strMLAWNumber);
                sqlComm.Parameters.AddWithValue("@MLAB_Number", "");
                sqlComm.Parameters.AddWithValue("@Address", strAddress);
                sqlComm.Parameters.AddWithValue("@City", strCity);
                sqlComm.Parameters.AddWithValue("@State", strState);
                sqlComm.Parameters.AddWithValue("@Zip", strZip);
                sqlComm.Parameters.AddWithValue("@Subdivision_Id", iSubdivision_Id == null ? (object)DBNull.Value : iSubdivision_Id);
                sqlComm.Parameters.AddWithValue("@Date_Received", dtDateReceived);
                sqlComm.Parameters.AddWithValue("@Order_Status_Id", iOrderStatusId);
                sqlComm.Parameters.AddWithValue("@Order_Warranty_Id", iWarrantyId == null ? (object)DBNull.Value : iWarrantyId);
                sqlComm.Parameters.AddWithValue("@Order_Type_Id", iOrderTypeId);
                sqlComm.Parameters.AddWithValue("@Plan_Number", strPlanNumber);
                sqlComm.Parameters.AddWithValue("@Plan_Name", strPlanName);
                sqlComm.Parameters.AddWithValue("@Lot", strLot);
                sqlComm.Parameters.AddWithValue("@Block", strBlock);
                sqlComm.Parameters.AddWithValue("@Section", strSection);
                sqlComm.Parameters.AddWithValue("@PI", strPI);
                sqlComm.Parameters.AddWithValue("@Visual_Geotec_Date", dtGeotecDate == null ? (object)DBNull.Value : dtGeotecDate);
                sqlComm.Parameters.AddWithValue("@Soils_Data_Source", strSoilsDataSource);
                sqlComm.Parameters.AddWithValue("@Fill_Applied", iFillApplied == null ? (object)DBNull.Value : iFillApplied);
                sqlComm.Parameters.AddWithValue("@Slope", strSlope);
                sqlComm.Parameters.AddWithValue("@Slab_Square_Feet", dSlabSquareFeet == null ? (object)DBNull.Value : dSlabSquareFeet);
                sqlComm.Parameters.AddWithValue("@Em_ctr", dEm_ctr == null ? (object)DBNull.Value : dEm_ctr);
                sqlComm.Parameters.AddWithValue("@Em_edg", dEm_edg == null ? (object)DBNull.Value : dEm_edg);
                sqlComm.Parameters.AddWithValue("@Ym_ctr", dYm_ctr == null ? (object)DBNull.Value : dYm_ctr);
                sqlComm.Parameters.AddWithValue("@Ym_edg", dYm_edg == null ? (object)DBNull.Value : dYm_edg);
                sqlComm.Parameters.AddWithValue("@Brg_cap", dBrg_cap == null ? (object)DBNull.Value : dBrg_cap);
                sqlComm.Parameters.AddWithValue("@SF", iSF == null ? (object)DBNull.Value : iSF);
                sqlComm.Parameters.AddWithValue("@Comments", strComments);
                sqlComm.Parameters.AddWithValue("@Elevation", strElevation);
                sqlComm.Parameters.AddWithValue("@Contact", strContact);
                sqlComm.Parameters.AddWithValue("@Phone", strPhone);
                sqlComm.Parameters.AddWithValue("@FoundationType", strFoundationType);
                sqlComm.Parameters.AddWithValue("@Phase", strPhase);

                sqlComm.Parameters.AddWithValue("@County", strCounty);
                sqlComm.Parameters.AddWithValue("@IsRevision", Convert.ToInt32(strIsRevision));
                sqlComm.Parameters.AddWithValue("@GarageType", strGarageType);
                sqlComm.Parameters.AddWithValue("@Patio", strPatio);
                sqlComm.Parameters.AddWithValue("@Fireplace", strFireplace);

                sqlComm.Parameters.AddWithValue("@Garage_Options", strGarageOptions);
                sqlComm.Parameters.AddWithValue("@Patio_Options", strPatioOptions);
                sqlComm.Parameters.AddWithValue("@Masonry_Sides", iMasonrySides);

                sqlComm.Parameters.AddWithValue("@Fill_Depth", strFillDepth);
                sqlComm.Parameters.AddWithValue("@Soils_Comments", strSoilsComments);
                sqlComm.Parameters.AddWithValue("@Customer_Job_Number", strCustomerJobNumber);
                sqlComm.Parameters.AddWithValue("@ParentId", iParentId == null ? (object)DBNull.Value : iParentId);

                sqlComm.Parameters.AddWithValue("@Amount", dAmount);
                sqlComm.Parameters.AddWithValue("@Discount", dDiscount);
                sqlComm.Parameters.AddWithValue("@FoundationOrderType", iFoundationOrderType);
                sqlComm.Parameters.AddWithValue("@DivisionId", iDivisionId == null ? (object)DBNull.Value : iDivisionId);
                sqlComm.Parameters.AddWithValue("@ClientId", iClientId == null ? (object)DBNull.Value : iClientId);

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);
            }

            return (ds);

        }


        public void insertOrderFile(int iOrderId, String strFileName)
        {
            if (strFileName != "")
            {
                var currentApplicationPath = HttpContext.Current.Request.PhysicalApplicationPath;
                var fullFilePath = currentApplicationPath + "uploads\\" + strFileName;

                byte[] bytes = File.ReadAllBytes(fullFilePath);
                SqlParameter fileP = new SqlParameter("@file", SqlDbType.VarBinary);
                fileP.Value = bytes;

                SqlParameter sqlOrderId = new SqlParameter("@Order_Id", SqlDbType.Int);
                sqlOrderId.Value = iOrderId;

                SqlParameter sqlFileName = new SqlParameter("@File_Name", SqlDbType.VarChar);
                sqlFileName.Value = strFileName;

                SqlCommand myCommand = new SqlCommand();
                myCommand.Parameters.Add(fileP);
                myCommand.Parameters.Add(sqlOrderId);
                myCommand.Parameters.Add(sqlFileName);

                SqlConnection conn = new SqlConnection(strConn);
                conn.Open();
                myCommand.Connection = conn;
                myCommand.CommandText = "Insert_Order_File";
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.ExecuteNonQuery();
                conn.Close();
            }
        }

        public DataSet getAllInspectors()
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_All_Inspectors", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);

            }
            return (ds);

        }

        public void insertInspectionOrder(int iOrderId, int iInspectionTypeId, int iIsReInspection, int iIsMultiPour, String strPhone, int iCanText, String strSpecialNotes, String strGEO, String strEmail, int iTimePref)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {

                SqlCommand sqlComm = new SqlCommand("Insert_Inspection_Order", conn);
                sqlComm.Parameters.AddWithValue("@Order_Id", iOrderId);
                sqlComm.Parameters.AddWithValue("@Inspection_Type_Id", iInspectionTypeId);
                sqlComm.Parameters.AddWithValue("@Is_Re_Inspection", iIsReInspection);
                sqlComm.Parameters.AddWithValue("@Is_Multi_Pour", iIsMultiPour);
                sqlComm.Parameters.AddWithValue("@Special_Notes", strSpecialNotes);
                sqlComm.Parameters.AddWithValue("@Phone", strPhone);
                sqlComm.Parameters.AddWithValue("@Can_Text", iCanText);
                sqlComm.Parameters.AddWithValue("@GEO", strGEO);
                sqlComm.Parameters.AddWithValue("@Email", strEmail);
                sqlComm.Parameters.AddWithValue("@TimePref", iTimePref);

                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }
        }

        public DataSet getClientInspectionPricing(int iClientId)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Client_Inspection_Pricing", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@Client_Id", iClientId);
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);

            }
            return (ds);
        }

        public DataSet getClientFoundationPricing(int iClientId)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Client_Foundation_Pricing", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@Client_Id", iClientId);
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);

            }
            return (ds);
        }

        public DataSet getInspectionTypes()
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Inspection_Types", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);

            }
            return (ds);

        }

        public DataSet getInspectionOrderSlice(string strStartDate, string strEndDate, string strInspectionTypes, string strInspectionStatuses)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Inspection_Order_Slice", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@StartDate", strStartDate);
                sqlComm.Parameters.AddWithValue("@EndDate", strEndDate);
                sqlComm.Parameters.AddWithValue("@Inspection_Status_Ids", strInspectionStatuses);
                sqlComm.Parameters.AddWithValue("@Inspection_Type_Ids", strInspectionTypes);
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);

            }
            return (ds);

        }


        public DataSet getInspectionOrder(int iInspectionOrderId)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Inspection_Order_By_Id", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@Inspection_Order_Id", iInspectionOrderId);
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);

            }
            return (ds);

        }

        public DataSet getDailyInspectionSchedule(DateTime dtStartDate)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Get_Complete_Daily_Schedule", conn);
                sqlComm.CommandType = CommandType.StoredProcedure;
                sqlComm.Parameters.AddWithValue("@StartDate", dtStartDate);
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);

            }
            return (ds);

        }


        public void updateInspectionOrder(int iOrderId, int iStatusId, int iTypeId, int iResult, string strResultNotes)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Update_Inspection_Order", conn);
                sqlComm.Parameters.AddWithValue("@Order_Id", iOrderId);
                sqlComm.Parameters.AddWithValue("@Order_Status_Id", iStatusId);
                sqlComm.Parameters.AddWithValue("@Inspection_Type_Id", iTypeId);
                sqlComm.Parameters.AddWithValue("@Inspection_Result", iResult);
                sqlComm.Parameters.AddWithValue("@Inspection_Result_Notes", strResultNotes);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }

        }



        public void insertClient(String strShortName, String strFullName, String strBillingAddress1, String strBillingAddress2, String strBillingCity, String strBillingState, String strBillingZip, String strPhone, String strFax, String strAttn, int iTurnAroundTime, int iPOFlag, int iInspectionFlag, String strVendorNumber, int iLowRange, int iHighRange, String strInvoiceNotes)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Insert_Client", conn);
                sqlComm.Parameters.AddWithValue("@Client_Short_Name", strShortName);
                sqlComm.Parameters.AddWithValue("@Client_Full_Name", strFullName);
                sqlComm.Parameters.AddWithValue("@Billing_Address_1", strBillingAddress1);
                sqlComm.Parameters.AddWithValue("@Billing_Address_2", strBillingAddress2);
                sqlComm.Parameters.AddWithValue("@Billing_City", strBillingCity);
                sqlComm.Parameters.AddWithValue("@Billing_State", strBillingState);
                sqlComm.Parameters.AddWithValue("@Billing_Postal_Code", strBillingZip);
                sqlComm.Parameters.AddWithValue("@Primary_Phone", strPhone);
                sqlComm.Parameters.AddWithValue("@Primary_Fax", strFax);
                sqlComm.Parameters.AddWithValue("@Attn", strAttn);
                sqlComm.Parameters.AddWithValue("@Turn_Around_Time", iTurnAroundTime);
                sqlComm.Parameters.AddWithValue("@PO_Flag",iPOFlag);
                sqlComm.Parameters.AddWithValue("@Inspection_Flag",iInspectionFlag);
                sqlComm.Parameters.AddWithValue("@Vendor_Number",strVendorNumber);
                sqlComm.Parameters.AddWithValue("@Low_Range",iLowRange);
                sqlComm.Parameters.AddWithValue("@High_Range",iHighRange);
                sqlComm.Parameters.AddWithValue("@Invoice_Notes",strInvoiceNotes);

                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }

        }

        public void insertWarrantyOrder(int iOrderId, string strWarrantyNotes)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Insert_Warranty_Order", conn);
                sqlComm.Parameters.AddWithValue("@Order_Id", iOrderId);
                sqlComm.Parameters.AddWithValue("@Warranty_Notes", strWarrantyNotes);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void updateWarrantyOrder(int iWarrantyOrderId, string strWarrantyNotes)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Update_Warranty_Order", conn);
                sqlComm.Parameters.AddWithValue("@Warranty_Order_Id", iWarrantyOrderId);
                sqlComm.Parameters.AddWithValue("@Warranty_Notes", strWarrantyNotes);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }

        }

        public void updateClient(int iClient_Id,String strShortName, String strFullName, String strBillingAddress1, String strBillingAddress2, String strBillingCity, String strBillingState, String strBillingZip, String strPhone, String strFax, String strAttn, String strVendorNumber, int iVPO)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Update_Client", conn);
                sqlComm.Parameters.AddWithValue("@Client_Id", iClient_Id);
                sqlComm.Parameters.AddWithValue("@Client_Short_Name", strShortName);
                sqlComm.Parameters.AddWithValue("@Client_Full_Name", strFullName);
                sqlComm.Parameters.AddWithValue("@Billing_Address_1", strBillingAddress1);
                sqlComm.Parameters.AddWithValue("@Billing_Address_2", strBillingAddress2);
                sqlComm.Parameters.AddWithValue("@Billing_City", strBillingCity);
                sqlComm.Parameters.AddWithValue("@Billing_State", strBillingState);
                sqlComm.Parameters.AddWithValue("@Billing_Postal_Code", strBillingZip);
                sqlComm.Parameters.AddWithValue("@Primary_Phone", strPhone);
                sqlComm.Parameters.AddWithValue("@Primary_Fax", strFax);
                sqlComm.Parameters.AddWithValue("@Attn", strAttn);
                sqlComm.Parameters.AddWithValue("@VPO", iVPO);
                

                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }

        }

        public void updateClientFoundationRevisionPricing(int iClientId, double dBase, double dNewHome)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Update_Client_Foundation_Revision_Pricing", conn);
                sqlComm.Parameters.AddWithValue("@Client_Id", iClientId);
                sqlComm.Parameters.AddWithValue("@Normal", dBase);
                sqlComm.Parameters.AddWithValue("@New_Home", dNewHome);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void deleteClientInspectionPricing(int iClientId)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Delete_Client_Inspection_Pricing", conn);
                sqlComm.Parameters.AddWithValue("@Client_Id", iClientId);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void deleteClientFoundationPricing(int iClientId)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Delete_Client_Foundation_Pricing", conn);
                sqlComm.Parameters.AddWithValue("@Client_Id", iClientId);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void insertClientInspectionPricing(int iClientId, int iInspectionTypeId, double dAmount, double dReinspAmount)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Insert_Client_Inspection_Pricing", conn);
                sqlComm.Parameters.AddWithValue("@Client_Id", iClientId);
                sqlComm.Parameters.AddWithValue("@Inspection_Type_Id", iInspectionTypeId);
                sqlComm.Parameters.AddWithValue("@Amount", dAmount);
                sqlComm.Parameters.AddWithValue("@ReinspAmount", dReinspAmount);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void insertClientFoundationPricing(int iClientId, int iPricingType, double dAmount, int iPricingTier, int iThreshold)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Insert_Client_Foundation_Pricing", conn);
                sqlComm.Parameters.AddWithValue("@Client_Id", iClientId);
                sqlComm.Parameters.AddWithValue("@Pricing_Type", iPricingType);
                sqlComm.Parameters.AddWithValue("@Amount", dAmount);
                sqlComm.Parameters.AddWithValue("@Pricing_Tier", iPricingTier);
                sqlComm.Parameters.AddWithValue("@Threshold", iThreshold);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }
        }


        public void deleteInspectorInspectionTypes(int iInspectorId)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Delete_Inspector_Inspection_Types", conn);
                sqlComm.Parameters.AddWithValue("@Inspector_Id", iInspectorId);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }

        }

        public void insertInspectorInsType(int iInspectorId, int iInspectionType)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Insert_Inspector_Inspection_Type", conn);
                sqlComm.Parameters.AddWithValue("@Inspector_Id", iInspectorId);
                sqlComm.Parameters.AddWithValue("@Inspection_Type_Id", iInspectionType);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }

        }

        public void upsertInspectorInfo(int iInspectorId, string strGeolocation)
        {
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand sqlComm = new SqlCommand("Upsert_Inspector_Info", conn);
                sqlComm.Parameters.AddWithValue("@Inspector_Id", iInspectorId);
                sqlComm.Parameters.AddWithValue("@Geolocation", strGeolocation);
                sqlComm.CommandType = CommandType.StoredProcedure;
                conn.Open();

                sqlComm.ExecuteNonQuery();
                conn.Close();
            }

        }
    }
}