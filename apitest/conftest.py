import pytest

@pytest.fixture
def api_url():
    return "https://hsourcerapp.azurewebsites.net/api"

@pytest.fixture
def api_version():
    return "v1.0"

@pytest.fixture
def url(api_url, api_version):
    return '{}/{}'.format(api_url, api_version)

@pytest.fixture
def username():
    return "devops@hscr.site"

@pytest.fixture
def password():
    return "Test12#$"

@pytest.fixture
def user_model():
    return {
        "userToken": "str",
        "id": "int",
        "active": "bool",
        "teamId": "int",
        "firstName": "str",
        "lastName": "str",
        "position": "str",
        "phoneNumber": "str",
        "email": "str",
        "photoPath": "str",
        "userRole": "str"
    }