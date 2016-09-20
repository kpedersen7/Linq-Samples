<Query Kind="Statements">
  <Connection>
    <ID>9f311933-f344-45e5-9620-c3568107f3e4</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//find waiter with most bills
var highestBillCount = from x in Waiters
//^^create variable called highestBillCount, start where you're selecting from, "Waiters"
						where x.Bills.Count() == (from y in Waiters
												select y.Bills.Count()).Max() 
						// ^^find the entry where the number of bills is equal to the max bill count
						select new {
							Name = x.FirstName + " " + x.LastName,
							BillCount = x.Bills.Count()
						}; 
						//^^select the first name and last name of that entry, as well as the bill count
	highestBillCount.Dump();
//----------------
//another way:
var maxBillCount = (from x in Waiters
					select x.Bills.Count()).Max();
var bestWaiter = from x in Waiters
					where x.Bills.Count == maxBillCount //comment this out to get bill counts for all waiters
					select new {
						Name = x.FirstName + " " + x.LastName,
						billcount = x.Bills.Count()
					};
	bestWaiter.Dump();
//----------------------
//create a dataset that has an unknown number of records associated with a data value
//the inner nested query uses the associated bill records of the currently (x) processing waiter
var waiterBills = from x in Waiters
//^^ remember that VARS hold datasets, not just ints or strings
					orderby x.LastName, x.FirstName
					select new {
						Name = x.LastName + ", " + x.FirstName,
						TotalBillCount = x.Bills.Count(),
						BillInfo = (from y in x.Bills
									where y.BillItems.Count() > 0
									select new {
									BillId = y.BillID,
									BillDate = y.BillDate,
									TableID = y.TableID,
									Total = y.BillItems.Sum(b => b.SalePrice * b.Quantity)
												}
									)
								};
	waiterBills.Dump();