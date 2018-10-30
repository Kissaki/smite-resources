import CRC32c
import JSON

function write(datafile, data)
    f = open(datafile, "w")
    JSON.print(f, data)
    close(f)
end

function writeifchanged(datafile, data)
    dataStr = JSON.json(data)
    crc = CRC32c.crc32c(dataStr)
    existing = open(CRC32c.crc32c, datafile)
    if crc != existing
        write(datafile, gods)
    end
end
