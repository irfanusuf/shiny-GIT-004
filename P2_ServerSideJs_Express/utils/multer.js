const multer = require("multer")




const upload = multer({
    dest : "/uploads",
    limits : {
        fieldSize : 1024 * 1024 * 100
    }
})



export const  uploadSingle  = upload.single("image")