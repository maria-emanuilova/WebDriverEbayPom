Feature: Ebay

Describes tests performed on Ebay website for searching and ordering items

@Ebay
	Scenario: Open Ebay page
	Given user tries to navigate to Ebay url
	When user loads the URL in the browser
	Then Ebay page is opened
	And close browser

	Scenario: Check search results
	Given user wants to search for an item in a category
	When user loads the URL in the browser
	And user loads selects category and enters a word to search
	Then relevant results are displayed with price and shipping information
	And close browser

	Scenario: Check item details
	Given user wants to see the details on a searched item
	When user loads the URL in the browser
	And user loads selects category and enters a word to search
	And user clicks on the item search result
	Then the title of the item and the price are correctly displayed
	And close browser

	Scenario: Check adding item to cart
	Given user wants to add item to cart
	When user loads the URL in the browser
	And user loads selects category and enters a word to search
	And user clicks on the item search result
	And user selects quantity and adds the item to cart
	Then user is redirected to the correct page
	And the order quantity is correct
	And the price is correct
	And close browser