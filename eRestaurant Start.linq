<Query Kind="Expression">
  <Connection>
    <ID>061b64b0-1e8f-45a0-8bcb-8ff763a1bc97</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

from food in items 
group food by new{food.MenuCategory.Description} into tempdataset
select new {
	MenuCategoryDescription = tempdataset.Key.Description,
	FoodItems = from x in tempdataset
				select new{
					ItemID = x.ItemID,
					FoodDescription = x.Description,
					CurrentPrice = x.CurrentPrice,
					TimesServed = x.BillItems.Count()
				}
}

from food in Items
orderby food.MenuCategory.Description
select new {
	MenuCategoryDescription = food.MenuCategory.Description;
	ItemID = food.ItemID,
	FoodDescription = food.Description,
	CurrentPrice = food.CurrentPrice,
	TimesServed = food.BillItems.Count()

}