Partial Public NotInheritable Class Utilities

  Public NotInheritable Class Conversions

    Public Shared ReadOnly Property KilogramsToPounds() As Double
      Get
        Return 2.2046
      End Get
    End Property

    Public Shared ReadOnly Property GramsToPounds() As Double
      Get
        Return KilogramsToPounds / 1000
      End Get
    End Property

    Public Shared ReadOnly Property PoundsToKilograms() As Double
      Get
        Return 0.4536
      End Get
    End Property

    Public Shared ReadOnly Property PoundsToGrams() As Double
      Get
        Return PoundsToKilograms * 1000
      End Get
    End Property

    Public Shared ReadOnly Property LitersToGallonsUK() As Double
      Get
        Return 0.219969
      End Get
    End Property

    Public Shared ReadOnly Property LitersToGallonsUS() As Double
      Get
        Return 0.264172
      End Get
    End Property

    Public Shared ReadOnly Property GallonsUSToLiters() As Double
      Get
        Return 3.785412
      End Get
    End Property

    Public Shared ReadOnly Property GallonsUKToLiters() As Double
      Get
        Return 4.54609
      End Get
    End Property

    Public Shared ReadOnly Property GallonsUKToGallonsUS() As Double
      Get
        Return 1.20094
      End Get
    End Property

    Public Shared ReadOnly Property GallonsUSToGallonsUK() As Double
      Get
        Return 0.83267
      End Get
    End Property

    Public Shared ReadOnly Property GallonsUSToPounds() As Double
      Get
        Return GallonsUSToLiters * KilogramsToPounds
      End Get
    End Property

    Public Shared ReadOnly Property PoundsPerGallonUSToGramsPerLiter() As Double
      Get
        Return PoundsToGrams / GallonsUSToLiters
      End Get
    End Property

    Public Shared ReadOnly Property MetersToYards() As Double
      Get
        Return 1.0936
      End Get
    End Property

    Public Shared ReadOnly Property YardsToMeters() As Double
      Get
        Return 0.9144
      End Get
    End Property

    Public Shared Function CentigradeToFarenheit(centigrade As Double) As Double
      ' Assumes tenths
      Return ((centigrade * 9) / 5) + 320
    End Function

    Public Shared Function CentigradeToFarenheit(centigrade As Short) As Short
      ' Assumes tenths
      Dim farenheit As Double = CentigradeToFarenheit(CDbl(centigrade))
      Return CShort(farenheit)
    End Function

    Public Shared Function FarenheitToCentigrade(farenheit As Double) As Double
      ' Assumes tenths
      Return ((farenheit - 320) / 9) * 5
    End Function

    Public Shared Function FarenheitToCentigrade(farenheit As Short) As Short
      ' Assumes tenths 
      Dim centrigrade As Double = FarenheitToCentigrade(CDbl(farenheit))
      Return CShort(centrigrade)
    End Function

  End Class
End Class
