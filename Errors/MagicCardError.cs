using MTGCardApi.Models;

namespace MTGCardApi.Errors;

public class MagicCardError : BaseError
{
	public MagicCardError(Guid magicCardId, ErrorType errorType, string message) : base(errorType, message)
	{
		MagicCardId = magicCardId;
		Metadata.Add(nameof(magicCardId), magicCardId);
	}

	public Guid MagicCardId { get; }

	public static MagicCardError CardNotFound(Guid magicCardId)
	{
		return new MagicCardError(magicCardId, ErrorType.DoesNotExist, $"{nameof(MagicCard)} was not found.");
	}
}
