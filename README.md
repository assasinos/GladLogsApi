# GladLogsApi

GladLogsApi is an API for retrieving and storing messages from Twitch chat, built using .NET 8 and C# 12.0.

## Features

- Connects to Twitch chat using a bot account.
- Retrieves messages from specified Twitch channels.
- Stores messages in a database.
- Provides endpoints to query messages and message counts.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Twitch account credentials, including:
  - A bot account.
  - An OAuth token for the bot account (available via [Twitch's token generator](https://twitchtokengenerator.com/)).

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/assasinos/GladLogsApi.git
   ```
2. **Install dependencies**

	Ensure all dependencies are restored. This can typically be done via Visual Studio or by running:
	```bash
	dotnet restore
	```	
3. Configure settings

- Update the appsettings.json file with:

	- Your Twitch bot credentials.
	- Database file path.
	- Secret key for secure operations.

4. Run the application
	```bash
	dotnet run
	```
## Frontend

The frontend for GladLogsApi is available in a separate repository. You can find it [here](TODO).

## Usage

### API Endpoints

Each endpoint serves a specific function within GladLogsApi. For endpoints requiring an AuthKey header, provide the secret key configured in appsettings.json.

- GET /weeks

	- **Description**: Retrieves active weeks for a user in a specific chat.

	- **Query Parameters**:
		- `UserName` (string): The name of the user.
		- `ChatName` (string): The name of the chat.

- GET /chats

	 - **Description**: Retrieves all chats.
	 
	 - **Parameters**: None.

- POST /chats

	- **Description**: Creates a new chat.

	- **Body Parameters**:
		- `Chatname` (string): The name of the chat to create.

	- **Headers**:
		- `AuthKey` (string): Required for authorization.

- DELETE /chats

	- **Description**: Deletes a specified chat.

	- **Body Parameters**:
		- `Chatname` (string): The name of the chat to delete.

	- **Headers**:
		- `AuthKey` (string): Required for authorization.

- GET /messages/count

	- **Description**: Retrieves the count of messages for a specific user in a specific chat.

	- **Query Parameters**:
		- `UserId` (string): The ID of the user.
		- `ChatId` (string): The ID of the chat.

- GET /messages

	- **Description**: Retrieves messages for a specific user in a specified chat and week.

	- **Query Parameters**:
		- `UserId` (string): The ID of the user.
		- `WeekId` (Guid): The ID of the week.
		- `ChatId` (string): The ID of the chat.

## Contributing

Contributions are welcome! To contribute:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/AmazingFeature`).
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`).
4. Push to the branch (`git push origin feature/AmazingFeature`).
5. Open a pull request.

For major changes, please open an issue first to discuss what you would like to change.

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgements

- TwitchLib: A C# library for interacting with Twitch chat and API.
