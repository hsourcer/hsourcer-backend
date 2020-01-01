import pytest

CONFIG = {
    "API_URL": "https://hsourcerapp.azurewebsites.net/api",
    "API_VERSION": "v1.0",
    "USERNAME": "devops@hscr.site",
    "ADMIN_USERNAME": "admin@hscr.site",
    "PASSWORD": "Test12#$",
    "USER_MODEL": {
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
    },
    "ORGANIZATION_ID": 1,
    "TEAM_ID": 1
}

@pytest.fixture
def config():
    return CONFIG

@pytest.fixture
def api_url():
    return CONFIG.get('API_URL', '')

@pytest.fixture
def api_version():
    return CONFIG.get('API_VERSION', '')

@pytest.fixture
def url(api_url, api_version):
    return '{}/{}'.format(api_url, api_version)

@pytest.fixture
def login_url(url):
    return '{}/{}/{}'.format(url, 'Auth', 'login')


@pytest.fixture
def username():
    return CONFIG.get('USERNAME', '')

@pytest.fixture
def admin_username():
    return CONFIG.get('ADMIN_USERNAME', '')

@pytest.fixture
def password():
    return CONFIG.get('PASSWORD', '')

@pytest.fixture
def user_model():
    return CONFIG.get('USER_MODEL', {})

@pytest.fixture
def organization_id():
    return CONFIG.get('ORGANIZATION_ID', 1)

@pytest.fixture
def team_id():
    return CONFIG.get('TEAM_ID', 1)