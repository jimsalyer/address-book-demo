# Address Book Demo API

## Start Database

```shellscript
docker-compose up
```

If you want to start up the database without locking your current console:

```shellscript
docker-compose up -d
```

## Stop Database

```shellscript
docker-compose down
```

If you want to remove any resources created during startup:

```shellscript
docker-compose down -v
```

## Start

```shellscript
dotnet run -p AddressBook.Api
```

## Start in Watch Mode

```shellscript
dotnet watch -p AddressBook.Api run
```

## Run Tests

```shellscript
dotnet test
```

## Swagger

Enter `http://localhost:5001/swagger` in your browser

### Authorize

- Expand `api/v1/users/authenticate` in the list of endpoints
- Click `Try Now` and enter the default administrator email address and password
  - Email address: `admin@admin.com`
  - Password: `@dmin123`
- Click `Execute`
- Copy the `accessToken` value from the JSON payload
- Click the `Authorize` button just above the list of endpoints
- In the box provided, enter `Bearer <ACCESS TOKEN YOU COPIED>` and click `Authorize`
- Close the popup
- You should now be able to access any of the endpoints

### Query Parameters

- `sorts` is a comma-delimited ordered list of property names to sort by. Adding a - before the name switches to sorting descendingly.
- `filters` is a comma-delimited list of `{Name}{Operator}{Value}` where
  - `{Name}` is the name of a property with the Sieve attribute or the name of a custom filter method for TEntity
    You can also have multiple names (for OR logic) by enclosing them in brackets and using a pipe delimiter, eg. `(LikeCount|CommentCount)>10` asks if `LikeCount` or `CommentCount` is `>10`
  - `{Operator}` is one of the [Operators](#operators)
  - `{Value}` is the value to use for filtering
    You can also have multiple values (for OR logic) by using a pipe delimiter, eg. `Title@=new|hot` will return posts with titles that contain the text "new" or "hot"

### Operators

| Operator | Meaning                                      |
| -------- | -------------------------------------------- |
| `==`     | Equals                                       |
| `!=`     | Not equals                                   |
| `>`      | Greater than                                 |
| `<`      | Less than                                    |
| `>=`     | Greater than or equal to                     |
| `<=`     | Less than or equal to                        |
| `@=`     | Contains                                     |
| `_=`     | Starts with                                  |
| `!@=`    | Does not Contains                            |
| `!_=`    | Does not Starts with                         |
| `@=*`    | Case-insensitive string Contains             |
| `_=*`    | Case-insensitive string Starts with          |
| `==*`    | Case-insensitive string Equals               |
| `!=*`    | Case-insensitive string Not equals           |
| `!@=*`   | Case-insensitive string does not Contains    |
| `!_=*`   | Case-insensitive string does not Starts with |
