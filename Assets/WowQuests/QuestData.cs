using System;
using Unity.VisualScripting;

[Serializable, Inspectable]
public class QuestData
{
	[Inspectable] public int questId;
	[Inspectable] public string questName;
	[Inspectable] public string questDescription;
	[Inspectable] public int itemId;
	[Inspectable] public int fulfillmentValue;
	
	public QuestData()
	{
	questId = -1;
	questName = "";
	questDescription = "";
	itemId = -1;
	fulfillmentValue = -1;
	}

	public QuestData(int questId, string questName, string questDescription, int itemId, int fulfillmentValue)
    {
		this.questId = questId;
		this.questName = questName;
		this.questDescription = questDescription;
		this.itemId = itemId;
		this.fulfillmentValue = fulfillmentValue;
    }
}
