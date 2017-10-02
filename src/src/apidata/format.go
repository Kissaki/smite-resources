package apidata

type Format struct {
	PathData string
}

func CreateFormatJson() Format {
	return Format{"json"}
}
func CreateFormatXml() Format {
	return Format{"xml"}
}

func (f *Format) ToString() string {
	return f.PathData
}
