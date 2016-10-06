<Query Kind="Expression">
  <Connection>
    <ID>061b64b0-1e8f-45a0-8bcb-8ff763a1bc97</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//multiple column groups

//grouping data places in a local temp data set for further processing
//.Key allows you to have access to the value(s) in your group key(s)
//if you have multiple group columns they MUST be in an anonymous datatype
// to create a DTO type collection you can use .ToList() on the temp data set
// you can have a custom anonymous data collection by using a nested query

//step A
from food in Items
group food by new{
					food.MenuCategoryID, 
					food.CurrentPrice
				}
				
//step B DTO style dataset
from food in Items
group food by new 
{
	food.MenuCategoryID, food.CurrentPrice
} into tempdataset
select new 
{
	MenuCategoryID = tempdataset.Key.MenuCategoryID,
	CurrentPrice = tempdataset.Key.CurrentPrice,
	FoodItems = tempdataset.ToList()
}

//Step C DTO style custom dataset
//use a nested query for the FoodItems
from food in Items
group food by new 
{
	food.MenuCategoryID, food.CurrentPrice
} into tempdataset
select new 
{
	MenuCategoryID = tempdataset.Key.MenuCategoryID,
	CurrentPrice = tempdataset.Key.CurrentPrice,
	FoodItems = from x in tempdataset
	select new
	{
		ItemID = x.ItemID,
		FoodDescription = x.Description,
		TimeServed = x.BillItems.Count()
	}
}