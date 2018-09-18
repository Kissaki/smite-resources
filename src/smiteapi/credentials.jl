import YAML

struct Credentials
    devid::Int
    authkey::String
end

const authfile = ".auth.yaml"

function loadcredentials()::Credentials
    data = YAML.load(open(authfile))
    Credentials(data["devid"], data["authkey"])
end
