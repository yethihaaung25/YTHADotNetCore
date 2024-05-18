namespace YTHADotNetCore.PizzaAPI.Queries
{
    public class PizzaQuery
    {
        public static string PizzaOrderQuery { get; } = @"SELECT		PO.*, P.Pizza, P.Price
	                                            FROM		Tbl_PizzaOrder PO
	                                            INNER JOIN	Tbl_Pizza P ON (PO.PizzaId = P.PizzaId)
	                                            WHERE		PO.PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo";

        public static string PizzaOrderDetailQuery { get; } = @"SELECT		OD.*, PE.PizzaExtraName,PE.Price
														FROM		Tbl_PizzaOrderDetail OD
														INNER JOIN	Tbl_PizzaExtra PE ON (OD.PizzaExtraId = PE.PizzaExtraId)
														WHERE		OD.PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo";
    }
}
