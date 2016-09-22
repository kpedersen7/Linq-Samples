<Query Kind="Program">
  <Connection>
    <ID>9f311933-f344-45e5-9620-c3568107f3e4</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

void Main()
{
//List of bill counts for all waiters
//This query will create a flat data set
//The columns are native datatypes ie int, string, double, etc
//One is not concerned with the repeated data in a column
var highestBillCount = from x in Waiters
						select new WaiterBillCounts{
							Name = x.FirstName + " " + x.LastName,
							TCount = x.Bills.Count()
						}; 
	highestBillCount.Dump();
	
var waiterBills = from x in Waiters
					where x.LastName.Contains("k")
					orderby x.LastName, x.FirstName
					select new WaiterBills{
						Name = x.LastName + ", " + x.FirstName,
						TotalBillCount = x.Bills.Count(),
						BillInfo = (from y in x.Bills
									where y.BillItems.Count() > 0
									select new BillItemSummary{
									BillId = y.BillID,
									BillDate = y.BillDate,
									TableID = y.TableID,
									Total = y.BillItems.Sum(b => b.SalePrice * b.Quantity)
												}
									).ToList()
								};
	waiterBills.Dump();
}

// Define other methods and classes here
//An example of a POCO class

public class WaiterBillCounts
{
	//whatever recieving field on your query in your select
	//there appears a property of that name in this class
	public string Name{get;set;}
	public int TCount{get;set;}
}

public class BillItemSummary
{
	public int BillId{get; set;}
	public DateTime BillDate{get; set;}
	public int? TableID{get; set;}
	public decimal Total{get; set;}
}

public class WaiterBills
{
	public string Name{get;set;}
	public int TotalBillCount{get;set;}
	public IEnumerable<BillItemSummary> BillInfo{get;set;}
}
